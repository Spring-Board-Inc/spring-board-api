using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.Response;
using Microsoft.EntityFrameworkCore;
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

        public async Task<ApiBaseResponse> Create(string userId, UserInformationDto dto)
        {
            if (!dto.IsValidParams)
                return new BadRequestResponse(ResponseMessages.InvalidRequest);

            var userInformation = _mapper.Map<UserInformation>(dto);
            userInformation.UserId = userId;
            await _repositoryManager.UserInformation.CreateUserInformationAsync(userInformation);
            await _repositoryManager.SaveAsync();

            var data = _mapper.Map<UserInformationToReturnDto>(userInformation);
            return new ApiOkResponse<UserInformationToReturnDto>(data);
        }

        public async Task<ApiBaseResponse> Get(string userId)
        {
            var userInfoQuery = _repositoryManager.UserInformation.FindUserInformation(userId, false);
            if (userInfoQuery == null)
                return new NotFoundResponse(ResponseMessages.UserInformationNotFound);

            var userInfoId = await userInfoQuery.Select(ui => ui.Id).FirstOrDefaultAsync();

            var userEducations = _repositoryManager.Education.GetEducations(userInfoId, false).OrderByDescending(ui => ui.StartDate);
            var userWorkExperiences = _repositoryManager.WorkExperience.FindExperiences(userInfoId, false).OrderByDescending(ui => ui.StartDate);
            var userCertifications = _repositoryManager.Certification.FindCertifications(userInfoId, false).OrderByDescending(ui => ui.IssuingDate);
            var userSkills = _repositoryManager.UserSkill.FindUserSkills(userInfoId, false);

            var userInfoToReturn = await (
                                        from userInfo in userInfoQuery
                                        join userEducation in userEducations on userInfo.Id equals userEducation.UserInformationId into ue
                                        from userEducation in ue.DefaultIfEmpty()
                                        join userWorkExperience in userWorkExperiences on userInfo.Id equals userWorkExperience.UserInformationId into uw
                                        from userWorkExperience in uw.DefaultIfEmpty()
                                        join userCertification in userCertifications on userInfo.Id equals userCertification.UserInformationId into uc
                                        from userCertification in uc.DefaultIfEmpty()
                                        join userSkill in userSkills on userInfo.Id equals userSkill.UserInformationId into us
                                        from userSkill in us.DefaultIfEmpty()
                                        select userInfo).FirstOrDefaultAsync();

            var data = _mapper.Map<UserInformationToReturn>(userInfoToReturn);
            return new ApiOkResponse<UserInformationToReturn?>(data);
        }

        public async Task<ApiBaseResponse> Update(Guid id, UserInformationDto dto)
        {
            if (!dto.IsValidParams)
                return new BadRequestResponse(ResponseMessages.InvalidRequest);

            var userInformation = await _repositoryManager.UserInformation.FindUserInformationAsync(id, true);
            if (userInformation == null)
                return new NotFoundResponse(ResponseMessages.UserInformationNotFound);

            userInformation.UpdatedAt = DateTime.Now;
            _mapper.Map(dto, userInformation);

            _repositoryManager.UserInformation.UpdateUserInformation(userInformation);
            await _repositoryManager.SaveAsync();

            return new ApiOkResponse<string>(ResponseMessages.UserInfoUpdated);
        }

        public async Task<ApiBaseResponse> Delete(Guid id)
        {
            var userInformation = await _repositoryManager.UserInformation.FindUserInformationAsync(id, true);
            if (userInformation == null)
                return new NotFoundResponse(ResponseMessages.UserInformationNotFound);

            _repositoryManager.UserInformation.DeleteUserInformation(userInformation);
            await _repositoryManager.SaveAsync();
            return new ApiOkResponse<string>(ResponseMessages.UserInfoDeleted);
        }

        public async Task<bool> Exists(string userId)
        {
            return await _repositoryManager.UserInformation.UserInformationExists(userId);
        }
    }
}