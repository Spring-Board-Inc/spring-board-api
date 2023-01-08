using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repositories.Extensions;
using Services.Contracts;
using Shared.DataTransferObjects;
using Shared.Helpers;
using Shared.RequestFeatures;
using System.Security.Claims;
using Entities.Enums;
using Microsoft.AspNetCore.Identity;

namespace Services
{
    public class JobService: IJobService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly UserManager<AppUser> userManager;

        public JobService(
            IRepositoryManager repository, 
            IMapper mapper,
            IEmailService emailService,
            UserManager<AppUser> userManager
            )
        {
            _repository = repository;
            _mapper = mapper;
            _emailService = emailService;
            this.userManager = userManager;
        }

        public async Task<ApiBaseResponse> Create(JobRequestObject request)
        {
            if (!request.IsValidSalaryRange)
                return new BadRequestResponse(ResponseMessages.InvalidSalaryRange);

            if (!request.IsValidClosingDate)
                return new BadRequestResponse(ResponseMessages.InvalidClosingDate);

            if (!request.IsValidParams)
                return new BadRequestResponse(ResponseMessages.InvalidRequest);

            var job = _mapper.Map<Job>(request);

            await _repository.Job.CreateJobAsync(job);
            await _repository.SaveAsync();

            var jobToReturn = _mapper.Map<JobMinimumInfoDto>(job);
            return new ApiOkResponse<JobMinimumInfoDto>(jobToReturn);
        }

        public async Task<ApiBaseResponse> Update(Guid id, JobRequestObject request)
        {
            if (!request.IsValidSalaryRange)
                return new BadRequestResponse(ResponseMessages.InvalidSalaryRange);

            if (!request.IsValidClosingDate)
                return new BadRequestResponse(ResponseMessages.InvalidClosingDate);

            if (!request.IsValidParams)
                return new BadRequestResponse(ResponseMessages.InvalidRequest);

            var jobForUpdate = await _repository.Job.FindJobAsync(id, true);
            if (jobForUpdate == null)
                return new NotFoundResponse(ResponseMessages.JobNotFound);

            _mapper.Map(request, jobForUpdate);

            jobForUpdate.UpdatedAt = DateTime.Now;
            _repository.Job.UpdateJob(jobForUpdate);
            await _repository.SaveAsync();

            var jobToReturn = _mapper.Map<JobMinimumInfoDto>(jobForUpdate);
            return new ApiOkResponse<JobMinimumInfoDto>(jobToReturn);
        }

        public async Task<ApiBaseResponse> Delete(Guid id)
        {
            var job = await _repository.Job.FindJobAsync(id, true);
            if (job == null)
                return new NotFoundResponse(ResponseMessages.JobNotFound);

            _repository.Job.DeleteJob(job);
            await _repository.SaveAsync();

            return new ApiOkResponse<string>(ResponseMessages.JobDeleted);
        }

        public async Task<ApiBaseResponse> Get(Guid id)
        {
            var job = await _repository.Job.FindJobAsync(id, true);
            if (job == null)
                return new NotFoundResponse(ResponseMessages.JobNotFound);

            return new ApiOkResponse<JobToReturnDto>(_mapper.Map<JobToReturnDto>(job));
        }

        public async Task<ApiBaseResponse> Get(SearchParameters searchParameters)
        {
            var endDate = searchParameters.EndDate == DateTime.MaxValue ? searchParameters.EndDate : searchParameters.EndDate.AddDays(1);
            var jobs = await _repository.Job.FindJobs(true)
                                    .Where(u => u.CreatedAt >= searchParameters.StartDate && u.CreatedAt <= endDate)
                                    .Include(j => j.Industry)
                                    .Include(j => j.Location)
                                    .Include(j => j.Company)
                                    .Include(j => j.Type)
                                    .Search(searchParameters.SearchBy)
                                    .OrderByDescending(j => j.CreatedAt)
                                    .ToListAsync();

            var jobDto = _mapper.Map<IEnumerable<JobToReturnDto>>(jobs);
            var pagedDataList = PagedList<JobToReturnDto>.Paginate(jobDto, searchParameters.PageNumber, searchParameters.PageSize);
            return new ApiOkResponse<PaginatedListDto<JobToReturnDto>>(pagedDataList);
        }

        public async Task<ApiBaseResponse> Get(JobSearchParams searchParams)
        {
            var jobQuery = _repository.Job.FindJobs(false);

            if (!searchParams.JobTypeId.Equals(Guid.Empty))
                jobQuery = jobQuery.Where(j => j.TypeId.Equals(searchParams.JobTypeId));

            if (!searchParams.IndustryId.Equals(Guid.Empty))
                jobQuery =  jobQuery.Where(j => j.IndustryId.Equals(searchParams.IndustryId));

            var jobs = await jobQuery.Search(searchParams.SearchBy)
                                    .Include(j => j.Industry)
                                    .Include(j => j.Company)
                                    .Include(j => j.Location)
                                    .Include(j => j.Type)
                                    .OrderByDescending(j => j.CreatedAt)
                                    .ToListAsync();

            var jobsToReturn = _mapper.Map<IEnumerable<JobToReturnDto>>(jobs);
            var pagedDataList = PagedList<JobToReturnDto>.Paginate(jobsToReturn, searchParams.PageNumber, searchParams.PageSize);
            return new ApiOkResponse<PaginatedListDto<JobToReturnDto>>(pagedDataList);
        }

        public async Task<ApiBaseResponse> Get(Guid companyId, SearchParameters searchParams)
        {
            var jobs = await _repository.Job.FindJobs(false)
                                        .Where(j => j.CompanyId.Equals(companyId))
                                        .Search(searchParams.SearchBy)
                                        .Include(j => j.Industry)
                                        .Include(j => j.Company)
                                        .Include(j => j.Location)
                                        .Include(j => j.Type)
                                        .ToListAsync();

            var jobsToReturn = _mapper.Map<IEnumerable<JobToReturnDto>>(jobs);
            var pagedData = PagedList<JobToReturnDto>.Paginate(jobsToReturn, searchParams.PageNumber, searchParams.PageSize);
            return new ApiOkResponse<PaginatedListDto<JobToReturnDto>>(pagedData);

        }

        public async Task<ApiBaseResponse> Get(string userId, SearchParameters searchParams)
        {
            var jobs = _repository.Job.FindJobs(false)
                                  .Search(searchParams.SearchBy)
                                  .Include(j => j.Industry)
                                  .Include(j => j.Company)
                                  .Include(j => j.Location)
                                  .Include(j => j.Type);

            var userJobs = _repository.UserJob.FindUserJobs(userId, false);

            var jobsAppliedFor = await (from job in jobs
                                 join userJob in userJobs on job.Id equals userJob.JobId
                                 orderby userJob.CreatedAt descending
                                 select job).ToListAsync();

            var jobsToReturn = _mapper.Map<IEnumerable<JobToReturnDto>>(jobsAppliedFor);
            var pagedData = PagedList<JobToReturnDto>.Paginate(jobsToReturn, searchParams.PageNumber, searchParams.PageSize);
            return new ApiOkResponse<PaginatedListDto<JobToReturnDto>>(pagedData);
        }

        public async Task<ApiBaseResponse> GetApplicants(Guid jobId, SearchParameters searchParams)
        {
            var userJobs = _repository.UserJob.FindUserJobs(jobId, false);
            var users = _repository.UserInformation.FindUserInformation(false);

            var usersInfo = await (from user in users
                        join userJob in userJobs on user.User.Id equals userJob.UserId
                        orderby userJob.CreatedAt ascending
                        select user).ToListAsync();

            var applicantsToReturn = _mapper.Map<IEnumerable<ApplicantInformation>>(usersInfo);
            var pagedData = PagedList<ApplicantInformation>.Paginate(applicantsToReturn, searchParams.PageNumber, searchParams.PageSize);
            return new ApiOkResponse<PaginatedListDto<ApplicantInformation>>(pagedData);
        }

        public async Task<ApiBaseResponse> Apply(Guid jobId, string applicantId, IFormFile cv)
        {
            var userInfo = await _repository.UserInformation.FindUserInformationAsync(applicantId, false);
            if (userInfo == null)
                return new NotFoundResponse(ResponseMessages.UserInformationNotFound);
            
            var job = await _repository.Job.FindJobAsync(jobId, true);
            if(job == null)
                return new NotFoundResponse(ResponseMessages.JobNotFound);

            var exists = await _repository.UserJob.Exists(applicantId, jobId);
            if (exists)
                return new BadRequestResponse(ResponseMessages.AlreadyApplied);
            
            if (job.ClosingDate < DateTime.Now)
                return new BadRequestResponse(ResponseMessages.JobExpired);
            
            var message = ApplicationMessage(job, userInfo);

            var isValidFile = Commons.ValidateDocumentFile(cv);
            if (!isValidFile.Successful)
                return new BadRequestResponse(isValidFile.Message);

            var isSent = await _emailService.SendMailAsync(new ApplicationRequestParameters
            {
                To = job.Company.Email,
                Subject = $"{userInfo.User.FirstName} {userInfo.User.LastName}: Application for the position of {job.Title}",
                Message = message,
                File = cv
            });

            if (!isSent)
                return new BadRequestResponse(ResponseMessages.ApplicationFailed);

            var userJob = new UserJob { UserId = applicantId, JobId = jobId };
            job.NumberOfApplicants++;

            await _repository.UserJob.CreateUserJob(userJob);
            await _repository.SaveAsync();

            return new ApiOkResponse<string>(ResponseMessages.ApplicationSuccessful);
        }

        public async Task<ApiBaseResponse> JobStats()
        {
            var allJobs = await _repository.Job.FindJobsAsync(false);
            var jobsCount = allJobs.Where(x => x.NumberOfApplicants > 0).Count();
            var activeJobsCount = allJobs.Where(x => x.ClosingDate > DateTime.Now).Count();
            var jobsFilled = jobsCount - activeJobsCount;
            jobsFilled = (jobsFilled < 0) ? 0 : jobsFilled;

            var companies = await _repository.Company.FindCompaniesAsync(false);
            var companiesCount = companies.Where(x => x.IsDeprecated == false).Count();

            var applicant = await userManager.GetUsersInRoleAsync(ERoles.Applicant.ToString());
            var applicantCount = applicant.Where(x => x.EmailConfirmed).Count();

            var jobStats = new JobStatsDto(applicantCount, activeJobsCount, jobsFilled, companiesCount);
            return new ApiOkResponse<JobStatsDto>(jobStats);
        }

        #region Private Methods
        private string? ApplicationMessage(Job job, UserInformation userInformation)
        {
            var possessivePronoun = userInformation.User.Gender == EGender.Male.ToString() ? "his" : "her";
            var objectivePronoun = userInformation.User.Gender == EGender.Male.ToString() ? "him" : "her";
            var hasPhoneNumber = !string.IsNullOrEmpty(userInformation.User.PhoneNumber);
            var whenHasPhone = hasPhoneNumber ? $" and phone number: {userInformation.User.PhoneNumber}" : "";

            var applicationDetails = new UserJobApplicationDetails 
            { 
                FirstName = userInformation.User.FirstName,
                LastName = userInformation.User.LastName,
                Email = userInformation.User.Email,
                JobTitle = job.Title,
                PossessivePronoun = possessivePronoun,
                ObjectivePronoun = objectivePronoun,
                WhenHasPhone = whenHasPhone
            };
            return GetEmailTemplates.GetJobApplicationEmailTemplate(applicationDetails);
        }
        #endregion
    }
}
