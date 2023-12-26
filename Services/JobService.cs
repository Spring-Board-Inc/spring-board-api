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
using Services.Extensions;

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
            await _repository.Job.AddAsync(job);
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

            var jobForUpdate = _repository.Job.FindAsync(id);
            if (jobForUpdate == null)
                return new NotFoundResponse(ResponseMessages.JobNotFound);

            _mapper.Map(request, jobForUpdate);
            jobForUpdate.UpdatedAt = DateTime.UtcNow;
            await _repository.Job.EditAsync(x => x.Id.Equals(id), jobForUpdate);
            return new ApiOkResponse<bool>(true);
        }

        public async Task<ApiBaseResponse> Delete(Guid id)
        {
            var job = _repository.Job.FindAsync(id);
            if (job == null)
                return new NotFoundResponse(ResponseMessages.JobNotFound);

            await _repository.Job.DeleteAsync(x => x.Id.Equals(id));
            return new ApiOkResponse<bool>(true);
        }

        public ApiBaseResponse Get(Guid id)
        {
            var job = _repository.Job.FindAsync(id);
            if (job == null)
                return new NotFoundResponse(ResponseMessages.JobNotFound);

            return new ApiOkResponse<JobToReturnDto>(_mapper.Map<JobToReturnDto>(job));
        }

        public ApiBaseResponse Get(SearchParameters searchParameters)
        {
            var jobs = _repository.Job.Find(searchParameters);

            var jobDto = _mapper.Map<IEnumerable<JobToReturnDto>>(jobs);
            var pagedDataList = PaginatedListDto<JobToReturnDto>.Paginate(jobDto, jobs.MetaData);
            pagedDataList.Data = AssignProps(pagedDataList.Data);
            return new ApiOkResponse<PaginatedListDto<JobToReturnDto>>(pagedDataList);
        }

        public ApiBaseResponse Get(JobSearchParams searchParams)
        {
            var searchTerms = new SearchParameters 
            { 
                PageNumber = searchParams.PageNumber, 
                PageSize = searchParams.PageSize, 
                SearchBy = searchParams.SearchBy 
            };

            var jobs = _repository.Job.FindNoDateFilter(searchTerms);

            var jobsToReturn = _mapper.Map<IEnumerable<JobToReturnDto>>(jobs);
            if (!searchParams.JobTypeId.Equals(Guid.Empty))
                jobsToReturn = jobsToReturn.Where(j => j.TypeId.Equals(searchParams.JobTypeId));

            if (!searchParams.IndustryId.Equals(Guid.Empty))
                jobsToReturn = jobsToReturn.Where(j => j.IndustryId.Equals(searchParams.IndustryId));

            var pagedDataList = PaginatedListDto<JobToReturnDto>.Paginate(jobsToReturn, jobs.MetaData);
            pagedDataList.Data = AssignProps(pagedDataList.Data);
            return new ApiOkResponse<PaginatedListDto<JobToReturnDto>>(pagedDataList);
        }

        public ApiBaseResponse Get(Guid companyId, SearchParameters searchParams)
        {
            var jobs = _repository.Job.Find(searchParams);

            var jobsToReturn = _mapper.Map<IEnumerable<JobToReturnDto>>(jobs)
                                    .Where(j => j.CompanyId.Equals(companyId));
            var pagedData = PaginatedListDto<JobToReturnDto>.Paginate(jobsToReturn, jobs.MetaData);
            pagedData.Data = AssignProps(pagedData.Data);
            return new ApiOkResponse<PaginatedListDto<JobToReturnDto>>(pagedData);

        }

        public ApiBaseResponse Get(string userId, SearchParameters searchParams)
        {
            var jobs = _repository.Job.Find(searchParams);

            var userJobsIds = _repository.UserJob.FindAsQueryable(userId).Select(i => i.JobId).ToList();
            var jobsToReturn = _mapper.Map<IEnumerable<JobToReturnDto>>(jobs);

            var jobsAppliedFor = (from job in jobsToReturn
                                 join userJobId in userJobsIds on job.Id equals userJobId
                                 orderby job.CreatedAt descending
                                 select job).ToList();

            var pagedData = PaginatedListDto<JobToReturnDto>.Paginate(jobsAppliedFor, jobs.MetaData);
            pagedData.Data = AssignProps(pagedData.Data);
            return new ApiOkResponse<PaginatedListDto<JobToReturnDto>>(pagedData);
        }

        public ApiBaseResponse GetApplicants(Guid jobId, SearchParameters searchParams)
        {
            var usersJobs = _repository.UserJob.FindByJobIdAsQueryable(jobId);
            var usersQuery = userManager.Users;

            var usersInfo = (from user in usersQuery
                        join userJob in usersJobs on user.Id equals userJob.UserId.StringToGuid()
                        orderby userJob.CreatedAt ascending
                        select user).Search(searchParams.SearchBy).ToList();

            var pagedList = PagedList<AppUser>.ToPagedList(usersInfo, searchParams.PageNumber, searchParams.PageSize);
            var applicantsToReturn = _mapper.Map<IEnumerable<ApplicantInformation>>(usersInfo);
            var pagedData = PaginatedListDto<ApplicantInformation>.Paginate(applicantsToReturn, pagedList.MetaData);

            var applicantsIds = pagedData.Data.Select(applicant => applicant.Id);
            var userInfoIds = pagedData.Data.Select(user => user.UserInformationId);

            var careerSummary = _repository.CareerSummary.FindAsQueryable(x => applicantsIds.Contains(x.UserId))
                .ToList();
            var education = _repository.Education.FindAsQueryable(x => userInfoIds.Contains(x.UserInformationId))
                .ToList();
            var experience = _repository.WorkExperience.FindAsQueryable(x => userInfoIds.Contains(x.UserInformationId))
                .ToList();
            var skills = _repository.UserSkill.FindAsQueryable(x => userInfoIds.Contains(x.UserInformationId))
                .ToList();
            var certifications = _repository.Certification.FindAsQueryable(x => userInfoIds.Contains(x.UserInformationId))
                .ToList();

            foreach (var user in pagedData.Data)
            {
                user.UserSkills = _mapper.Map<ICollection<UserSkillMinInfo>>(skills.Select(x => x.UserInformationId.Equals(user.UserInformationId)));
                user.CareerSummary = careerSummary.FirstOrDefault(x => x.UserId.Equals(user.Id))?.Summary;
                user.Educations = _mapper.Map<ICollection<EducationMinInfo>>(education.Select(x => x.UserInformationId.Equals(user.UserInformationId)));
                user.WorkExperiences = _mapper.Map<ICollection<WorkExperienceMinInfo>>(experience.Select(x => x.UserInformationId.Equals(user.UserInformationId)));
                user.Certifications = _mapper.Map<ICollection<CertificationMinInfo>>(certifications.Select(x => x.UserInformationId.Equals(user.UserInformationId)));
            }

            return new ApiOkResponse<PaginatedListDto<ApplicantInformation>>(pagedData);
        }

        public async Task<ApiBaseResponse> GetApplicant(Guid jobId, Guid applicantId)
        {
            var userJob = _repository.UserJob
                .FindAsQueryable(jobId, applicantId)
                .FirstOrDefault();

            if (userJob == null)
                return new BadRequestResponse(ResponseMessages.JobNotFound);

            var user = userManager.Users
                .Where(u => u.Id.Equals(userJob.UserId))
                .FirstOrDefault();

            var userInfo = await _repository.UserInformation.GetByUserIdAsync(applicantId);
            if (user == null || userInfo == null)
                return new BadRequestResponse(ResponseMessages.UserNotFound);

            var careerSummary = await _repository.CareerSummary.FindAsync(x => x.UserId.Equals(applicantId));
            var education = _repository.Education.FindByUserInfoId(userInfo.Id);
            var experience = _repository.WorkExperience.FindByUserInfoId(userInfo.Id);
            var skills = _repository.UserSkill.FindAsList(userInfo.Id);
            var certifications = _repository.Certification
                .FindByUserInfoIdAsQueryable(userInfo.Id)
                .ToList();

            var data = _mapper.Map<ApplicantInformation>(user);
            data.CareerSummary = careerSummary.Summary;
            data.Educations = _mapper.Map<ICollection<EducationMinInfo>>(education);
            data.WorkExperiences = _mapper.Map<ICollection<WorkExperienceMinInfo>>(experience);
            data.Certifications = _mapper.Map<ICollection<CertificationMinInfo>>(certifications);
            data.UserSkills = _mapper.Map<ICollection<UserSkillMinInfo>>(skills);

            return new ApiOkResponse<ApplicantInformation>(data);
        }

        public async Task<ApiBaseResponse> Apply(Guid jobId, string applicantId, CvToSendDto dto)
        {
            if (!dto.IsValidParams)
                return new BadRequestResponse(ResponseMessages.InvalidRequest);

            var userInfo = await _repository.UserInformation.GetByUserIdAsync(Guid.Parse(applicantId));
            var user = await userManager.FindByIdAsync(userInfo.UserId.ToString());
            if (userInfo == null || user == null)
                return new NotFoundResponse(ResponseMessages.UserInformationNotFound);
            
            var job = _repository.Job.FindAsync(jobId);
            if(job == null)
                return new NotFoundResponse(ResponseMessages.JobNotFound);

            var company = await _repository.Company.FindAsync(job.CompanyId);
            if (company == null)
                return new NotFoundResponse(ResponseMessages.CompanyNotFound);

            var exists = await _repository.UserJob.Exists(applicantId, jobId);
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
                To = company.Email,
                Subject = $"{user.FirstName} {user.LastName}: Application for the position of {job.Title}",
                Message = message,
                File = dto.Cv
            });

            if (!isSent)
                return new BadRequestResponse(ResponseMessages.ApplicationFailed);

            var userJob = new UserJob { UserId = applicantId, JobId = jobId };
            job.NumberOfApplicants++;

            await _repository.Job.EditAsync(x => x.Id.Equals(jobId), job);
            await _repository.UserJob.AddAsync(userJob);

            return new ApiOkResponse<bool>(true);
        }

        public async Task<ApiBaseResponse> JobStats()
        {
            var allJobs = _repository.Job.FindAsQueryable();
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

        private IEnumerable<JobToReturnDto> AssignProps(IEnumerable<JobToReturnDto> data)
        {
            IEnumerable<Guid> industriesIds = new List<Guid>();
            IEnumerable<Guid> jobTypesIds = new List<Guid>();
            IEnumerable<Guid> statesIds = new List<Guid>();
            IEnumerable<Guid> countriesIds = new List<Guid>();
            IEnumerable<Guid> companiesIds = new List<Guid>();

            foreach (var item in data)
            {
                industriesIds = data.Select(x => x.IndustryId);
                jobTypesIds = data.Select(x => x.TypeId);
                countriesIds = data.Select(x => x.CountryId);
                statesIds = data.Select(x => x.StateId);
                companiesIds = data.Select(x => x.CompanyId);
            }

            var industries = _repository.Industry.FindAsQueryable()
                .Where(x => industriesIds.Contains(x.Id))
                .ToList();
            var jobTypes = _repository.JobType.FindAsQueryable()
                .Where(x => jobTypesIds.Contains(x.Id))
                .ToList();
            var countries = _repository.Country.FindAsQueryable()
                .Where(x => countriesIds.Contains(x.Id))
                .ToList();
            var states = _repository.State
                .FindAsQueryable(x => statesIds.Contains(x.Id))
                .ToList();
            var companies = _repository.Company
                .FindAsQueryable(x => companiesIds.Contains(x.Id))
                .ToList();

            foreach (var job in data)
            {
                var company = companies.Where(x => x.Id == job.CompanyId).FirstOrDefault();
                job.Industry = industries.Where(x => x.Id == job.IndustryId).FirstOrDefault()?.Name;
                job.JobType = jobTypes.Where(x => x.Id == job.TypeId).FirstOrDefault()?.Name;
                job.State = states.Where(x => x.Id == job.StateId).FirstOrDefault()?.AdminArea;
                job.Country = countries.Where(x => x.Id == job.CountryId).FirstOrDefault()?.Name;
                job.LogoUrl = company?.LogoUrl;
                job.Company = company?.Name;
                job.Email = company?.Email;
            }

            return data;
        }
        #endregion
    }
}
