using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.Response;
using Services.Contracts;
using Shared.DataTransferObjects;
using Shared.Helpers;

namespace Services
{
    public class UserInformationService : IUserInformationService
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;

        public UserInformationService(ILoggerManager logger, IMapper mapper, IRepositoryManager repositoryManager)
        {
            _logger = logger;
            _mapper = mapper;
            _repositoryManager = repositoryManager;
        }

        public async Task<ApiBaseResponse> Create(Guid userId)
        {
            var userInformation = new UserInformation
            {
                UserId = userId
            };
            await _repositoryManager.UserInformation.AddAsync(userInformation);
            var data = _mapper.Map<UserInformationToReturnDto>(userInformation);
            return new ApiOkResponse<UserInformationToReturnDto>(data);
        }

        public async Task<ApiBaseResponse> Get(Guid userId)
        {
            var userInfo = await _repositoryManager.UserInformation
                .GetByUserIdAsync(userId);
            if (userInfo == null)
                return new NotFoundResponse(ResponseMessages.UserInformationNotFound);

            var userEducations = _repositoryManager.Education.FindByUserInfoIdAsQueryable(userInfo.Id)
                                                   .OrderByDescending(ui => ui.StartDate)
                                                   .ToList();
            var userWorkExperiences = _repositoryManager.WorkExperience.FindByUserInfoIdAsQueryable(userInfo.Id)
                                                   .ToList();
            var userCertifications = _repositoryManager.Certification.FindByUserInfoIdAsQueryable(userInfo.Id)
                                                   .ToList();
            var userSkills = _repositoryManager.UserSkill.FindAsQueryable(userInfo.Id)
                                                   .ToList();

            userInfo.Educations = userEducations;
            userInfo.WorkExperiences = userWorkExperiences;
            userInfo.Certifications = userCertifications;
            userInfo.UserSkills = userSkills;

            var data = _mapper.Map<UserInformationToReturn>(userInfo);
            return new ApiOkResponse<UserInformationToReturn?>(data);
        }

        public async Task<ApiBaseResponse> Delete(Guid id)
        {
            var userInformation = await _repositoryManager.UserInformation.GetByIdAsync(id);
            if (userInformation == null)
                return new NotFoundResponse(ResponseMessages.UserInformationNotFound);

            await _repositoryManager.UserInformation.DeleteAsync(x => x.Id.Equals(userInformation.Id));
            return new ApiOkResponse<string>(ResponseMessages.UserInfoDeleted);
        }

        public async Task<bool> Exists(Guid userId)
        {
            return await _repositoryManager.UserInformation.ExistsAsync(userId);
        }
    }
}