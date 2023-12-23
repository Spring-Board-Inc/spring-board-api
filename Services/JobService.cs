using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.Response;
using Microsoft.EntityFrameworkCore;
using Repositories.Extensions;
using Services.Contracts;
using Shared.DataTransferObjects;
using Shared.Helpers;
using Shared.RequestFeatures;
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

            return new ApiOkResponse<bool>(true);
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

            return new ApiOkResponse<bool>(true);
        }

        public async Task<ApiBaseResponse> Delete(Guid id)
        {
            var job = await _repository.Job.FindJobAsync(id, true);
            if (job == null)
                return new NotFoundResponse(ResponseMessages.JobNotFound);

            _repository.Job.DeleteJob(job);
            await _repository.SaveAsync();

            return new ApiOkResponse<bool>(true);
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
            var jobs = await _repository.Job.FindJobs(searchParameters);

            var jobDto = _mapper.Map<IEnumerable<JobToReturnDto>>(jobs);
            var pagedDataList = PaginatedListDto<JobToReturnDto>.Paginate(jobDto, jobs.MetaData);
            return new ApiOkResponse<PaginatedListDto<JobToReturnDto>>(pagedDataList);
        }

        public async Task<ApiBaseResponse> Get(JobSearchParams searchParams)
        {
            var searchTerms = new SearchParameters 
            { 
                PageNumber = searchParams.PageNumber, 
                PageSize = searchParams.PageSize, 
                SearchBy = searchParams.SearchBy 
            };

            var jobs = await _repository.Job.FindJobs(searchTerms, false);

            var jobsToReturn = _mapper.Map<IEnumerable<JobToReturnDto>>(jobs);
            if (!searchParams.JobTypeId.Equals(Guid.Empty))
                jobsToReturn = jobsToReturn.Where(j => j.TypeId.Equals(searchParams.JobTypeId));

            if (!searchParams.IndustryId.Equals(Guid.Empty))
                jobsToReturn = jobsToReturn.Where(j => j.IndustryId.Equals(searchParams.IndustryId));

            var pagedDataList = PaginatedListDto<JobToReturnDto>.Paginate(jobsToReturn, jobs.MetaData);
            return new ApiOkResponse<PaginatedListDto<JobToReturnDto>>(pagedDataList);
        }

        public async Task<ApiBaseResponse> Get(Guid companyId, SearchParameters searchParams)
        {
            var jobs = await _repository.Job.FindJobs(searchParams, false);

            var jobsToReturn = _mapper.Map<IEnumerable<JobToReturnDto>>(jobs)
                                    .Where(j => j.CompanyId.Equals(companyId));
            var pagedData = PaginatedListDto<JobToReturnDto>.Paginate(jobsToReturn, jobs.MetaData);
            return new ApiOkResponse<PaginatedListDto<JobToReturnDto>>(pagedData);

        }

        public async Task<ApiBaseResponse> Get(string userId, SearchParameters searchParams)
        {
            var jobs = await _repository.Job.FindJobs(searchParams, false);

            var userJobsIds = await _repository.UserJob.FindUserJobs(userId, false).Select(i => i.JobId).ToListAsync();
            var jobsToReturn = _mapper.Map<IEnumerable<JobToReturnDto>>(jobs);

            var jobsAppliedFor = (from job in jobsToReturn
                                 join userJobId in userJobsIds on job.Id equals userJobId
                                 orderby job.CreatedAt descending
                                 select job).ToList();

            var pagedData = PaginatedListDto<JobToReturnDto>.Paginate(jobsAppliedFor, jobs.MetaData);
            return new ApiOkResponse<PaginatedListDto<JobToReturnDto>>(pagedData);
        }

        public async Task<ApiBaseResponse> GetApplicants(Guid jobId, SearchParameters searchParams)
        {
            var userJobs = _repository.UserJob.FindUserJobs(jobId, false);
            var users = userManager.Users
                .Include(x => x.UserInformation.Educations)
                .Include(x => x.UserInformation.WorkExperiences)
                .Include(x => x.UserInformation.UserSkills)
                .Include(x => x.UserInformation.Certifications)
                .Include(x => x.CareerSummary);

            var usersInfo = await (from user in users
                        //join userJob in userJobs on user.Id equals userJob.UserId
                        //orderby userJob.CreatedAt ascending
                        select user).Search(searchParams.SearchBy).ToListAsync();

            var pagedList = PagedList<AppUser>.ToPagedList(usersInfo, searchParams.PageNumber, searchParams.PageSize);

            var applicantsToReturn = _mapper.Map<IEnumerable<ApplicantInformation>>(usersInfo);
            var pagedData = PaginatedListDto<ApplicantInformation>.Paginate(applicantsToReturn, pagedList.MetaData);
            return new ApiOkResponse<PaginatedListDto<ApplicantInformation>>(pagedData);
        }

        public async Task<ApiBaseResponse> GetApplicant(Guid jobId, Guid applicantId)
        {
            var userJobQuery = _repository.UserJob.FindUserJob(jobId, applicantId, false);
            var userQuery = userManager.Users
                .Include(u => u.UserInformation.Educations)
                .Include(u => u.UserInformation.WorkExperiences)
                .Include(u => u.UserInformation.UserSkills)
                .Include(u => u.UserInformation.Certifications)
                .Include(u => u.CareerSummary)
                .Where(u => u.Id.Equals(applicantId.ToString()));

            var singleUser = await (from user in userQuery
                              //join userJob in userJobQuery on user.Id equals userJob.UserId
                              select user).FirstOrDefaultAsync();

            if (singleUser == null)
                return new BadRequestResponse(ResponseMessages.UserNotFound);

            return new ApiOkResponse<ApplicantInformation>(_mapper.Map<ApplicantInformation>(singleUser));
        }

        public async Task<ApiBaseResponse> Apply(Guid jobId, string applicantId, CvToSendDto dto)
        {
            if (!dto.IsValidParams)
                return new BadRequestResponse(ResponseMessages.InvalidRequest);

            var userInfo = await _repository.UserInformation.GetByUserIdAsync(Guid.Parse(applicantId));
            var user = await userManager.FindByIdAsync(userInfo.UserId.ToString());
            if (userInfo == null || user == null)
                return new NotFoundResponse(ResponseMessages.UserInformationNotFound);
            
            var job = await _repository.Job.FindJobAsync(jobId, true);
            if(job == null)
                return new NotFoundResponse(ResponseMessages.JobNotFound);

            var exists = await _repository.UserJob.Exists(applicantId.ToString(), jobId);
            if (exists)
                return new BadRequestResponse(ResponseMessages.AlreadyApplied);
            
            if (job.ClosingDate < DateTime.Now)
                return new BadRequestResponse(ResponseMessages.JobExpired);
            
            var message = await ApplicationMessage(job, userInfo);

            var isValidFile = Commons.ValidateDocumentFile(dto.Cv);
            if (!isValidFile.Successful)
                return new BadRequestResponse(isValidFile.Message);

            var isSent = await _emailService.SendMailAsync(new ApplicationRequestParameters
            {
                To = job.Company.Email,
                Subject = $"{user.FirstName} {user.LastName}: Application for the position of {job.Title}",
                Message = message,
                File = dto.Cv
            });

            if (!isSent)
                return new BadRequestResponse(ResponseMessages.ApplicationFailed);

            var userJob = new UserJob { UserId = applicantId.ToString(), JobId = jobId };
            job.NumberOfApplicants++;

            await _repository.UserJob.CreateUserJob(userJob);
            await _repository.SaveAsync();

            return new ApiOkResponse<bool>(true);
        }

        public async Task<ApiBaseResponse> JobStats()
        {
            var allJobs = await _repository.Job.FindJobsAsync(false);
            var activeJobs = allJobs.Where(x => x.ClosingDate >= DateTime.Now);
            var activeJobsNumbersToBeHired = activeJobs.Sum(x => x.NumbersToBeHired);
            int activeJobsCount = activeJobs.Count();

            IEnumerable<Job> closedJobs = allJobs.Where(x => x.ClosingDate <= DateTime.Now);
            int jobsFilled = 0;
            foreach (Job job in closedJobs)
            {
                jobsFilled += job?.NumberOfApplicants < job?.NumbersToBeHired ? job.NumberOfApplicants : job.NumbersToBeHired;
            }

            var companyCount = await _repository.Company.Count(x => x.IsDeprecated == false);

            var applicant = await userManager.GetUsersInRoleAsync(ERoles.Applicant.ToString());
            var applicantCount = applicant.Where(x => x.EmailConfirmed).Count();

            var jobStats = new JobStatsDto(applicantCount, activeJobsCount, jobsFilled, companyCount);
            return new ApiOkResponse<JobStatsDto>(jobStats);
        }

        #region Private Methods
        private async Task<string?> ApplicationMessage(Job job, UserInformation userInformation)
        {
            var user = await userManager.FindByIdAsync(userInformation.UserId.ToString());
            var possessivePronoun = user.Gender == EGender.Male.ToString() ? "his" : "her";
            var objectivePronoun = user.Gender == EGender.Male.ToString() ? "him" : "her";
            var hasPhoneNumber = !string.IsNullOrEmpty(user.PhoneNumber);
            var whenHasPhone = hasPhoneNumber ? $" and phone number: {user.PhoneNumber}" : "";

            var applicationDetails = new UserJobApplicationDetails 
            { 
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
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
