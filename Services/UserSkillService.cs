using AutoMapper;
using Contracts;
using Entities.Enums;
using Entities.Models;
using Entities.Response;
using Services.Contracts;
using Shared.DataTransferObjects;
using Shared.Helpers;

namespace Services
{
    public class UserSkillService : IUserSkillService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public UserSkillService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiBaseResponse> Create(Guid userInfoId, Guid skillId, UserSkillRequest request)
        {
            if (!request.IsValidParams)
                return new BadRequestResponse(ResponseMessages.InvalidRequest);

            request.Level = Commons.Capitalize(request.Level);

            if (
                skillId == Guid.Empty ||
                userInfoId == Guid.Empty ||
                !Enum.IsDefined(typeof(ESkillLevel), request.Level)
                )
                return new BadRequestResponse(ResponseMessages.InvalidRequest);

            var skill = await _repository.Skills.FindByIdAsync(skillId);
            if (skill == null)
                return new BadRequestResponse(ResponseMessages.InvalidRequest);

            var userSkill = _mapper.Map<UserSkill>(request);
            userSkill.Skill = skill.Description;
            userSkill.SkillId = skillId;
            userSkill.UserInformationId = userInfoId;

            await _repository.UserSkill.CreateUserSkill(userSkill);
            await _repository.SaveAsync();

            return new ApiOkResponse<UserSkill>(userSkill);
        }

        public async Task<ApiBaseResponse> Delete(Guid userInfoId, Guid skillId)
        {
            var userSkill = await _repository.UserSkill.FindUserSkillAsync(userInfoId, skillId, true);
            if (userSkill == null)
                return new BadRequestResponse(ResponseMessages.UserSkillNotFound);

            _repository.UserSkill.DeleteUserSkill(userSkill);
            await _repository.SaveAsync();

            return new ApiOkResponse<string>(ResponseMessages.NoContent);
        }

        public async Task<ApiBaseResponse> Get(Guid userInfoId, Guid skillId)
        {
            var userSkill = await _repository.UserSkill.FindUserSkillAsync(userInfoId, skillId, false);
            if (userSkill == null)
                return new BadRequestResponse(ResponseMessages.UserSkillNotFound);

            var data = _mapper.Map<UserSkillMinInfo>(userSkill);
            return new ApiOkResponse<UserSkillMinInfo>(data);
        }

        public async Task<IEnumerable<UserSkillMinInfo>> Get(Guid userInfoId)
        {
            var userSkills = await _repository.UserSkill.FindUserSkillsAsync(userInfoId, false);
            return _mapper.Map<IEnumerable<UserSkillMinInfo>>(userSkills);
        }

        public async Task<ApiBaseResponse> Update(Guid userInfoId, Guid skillId, UserSkillRequest request)
        {
            if (!request.IsValidParams)
                return new BadRequestResponse(ResponseMessages.InvalidRequest);

            request.Level = Commons.Capitalize(request.Level);

            if (
                skillId == Guid.Empty || 
                userInfoId == Guid.Empty || 
                !Enum.IsDefined(typeof(ESkillLevel), request.Level)
                )
                return new BadRequestResponse(ResponseMessages.InvalidRequest);

            var userSkill = await _repository.UserSkill.FindUserSkillAsync(userInfoId, skillId, true);
            if (userSkill == null)
                return new NotFoundResponse(ResponseMessages.UserSkillNotFound);

            userSkill.Level = request.Level;
            _repository.UserSkill.UpdateUserSkill(userSkill);
            await _repository.SaveAsync();

            return new ApiOkResponse<string>(ResponseMessages.NoContent);
        }
    }
}