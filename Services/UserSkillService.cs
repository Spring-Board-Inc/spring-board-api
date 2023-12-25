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

            if (!Enum.IsDefined(typeof(ESkillLevel), request.Level))
                return new BadRequestResponse(ResponseMessages.InvalidSkillLevel);

            var skill = await _repository.Skills.FindByIdAsync(skillId);
            if (skill == null)
                return new BadRequestResponse(ResponseMessages.SkillNotFound);

            if (!await _repository.UserInformation.Exists(x => x.Id.Equals(userInfoId)))
                return new BadRequestResponse(ResponseMessages.UserInformationNotFound);

            if (await _repository.UserSkill.Exists(x => x.UserInformationId.Equals(userInfoId) && x.SkillId.Equals(skill.Id)))
                return new BadRequestResponse(ResponseMessages.UserSkillExists);

            var userSkill = _mapper.Map<UserSkill>(request);
            userSkill.Skill = skill.Description;
            userSkill.SkillId = skill.Id;
            userSkill.UserInformationId = userInfoId;

            await _repository.UserSkill.AddAsync(userSkill);
            return new ApiOkResponse<UserSkill>(userSkill);
        }

        public async Task<ApiBaseResponse> Delete(Guid userInfoId, Guid skillId)
        {
            var userSkill = await _repository.UserSkill.FindAsync(userInfoId, skillId);
            if (userSkill == null)
                return new BadRequestResponse(ResponseMessages.UserSkillNotFound);

            await _repository.UserSkill
                .DeleteAsync(x => x.UserInformationId.Equals(userInfoId) && x.SkillId.Equals(skillId));
            return new ApiOkResponse<string>(ResponseMessages.NoContent);
        }

        public async Task<ApiBaseResponse> Get(Guid userInfoId, Guid skillId)
        {
            var userSkill = await _repository.UserSkill.FindAsync(userInfoId, skillId);
            if (userSkill == null)
                return new BadRequestResponse(ResponseMessages.UserSkillNotFound);

            var data = _mapper.Map<UserSkillMinInfo>(userSkill);
            return new ApiOkResponse<UserSkillMinInfo>(data);
        }

        public IEnumerable<UserSkillMinInfo> Get(Guid userInfoId)
        {
            var userSkills = _repository.UserSkill.FindAsList(userInfoId);
            return _mapper.Map<IEnumerable<UserSkillMinInfo>>(userSkills);
        }

        public async Task<ApiBaseResponse> Update(Guid userInfoId, Guid skillId, UserSkillRequest request)
        {
            if (!request.IsValidParams)
                return new BadRequestResponse(ResponseMessages.InvalidRequest);

            request.Level = Commons.Capitalize(request.Level);

            if (!Enum.IsDefined(typeof(ESkillLevel), request.Level))
                return new BadRequestResponse(ResponseMessages.InvalidSkillLevel);

            if (!await _repository.Skills.Exists(x => x.Id.Equals(skillId)))
                return new BadRequestResponse(ResponseMessages.SkillNotFound);

            if (!await _repository.UserInformation.Exists(x => x.Id.Equals(userInfoId)))
                return new BadRequestResponse(ResponseMessages.UserInformationNotFound);

            var userSkill = await _repository.UserSkill.FindAsync(userInfoId, skillId);
            if (userSkill == null)
                return new NotFoundResponse(ResponseMessages.UserSkillNotFound);

            userSkill.Level = request.Level;
            await _repository.UserSkill
                .EditAsync(x => x.UserInformationId.Equals(userInfoId) && x.SkillId.Equals(skillId), userSkill);
            return new ApiOkResponse<string>(ResponseMessages.UserSkillUpdated);
        }
    }
}