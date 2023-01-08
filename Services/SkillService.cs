using AutoMapper;
using Contracts;
using Entities.Enums;
using Entities.Models;
using Entities.Response;
using Microsoft.EntityFrameworkCore;
using Services.Contracts;
using Shared.DataTransferObjects;
using Shared.Helpers;

namespace Services
{
    public class SkillService : ISkillService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public SkillService
        (
            IRepositoryManager repository,
            IMapper mapper
        )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiBaseResponse> Create(SkillRequest request)
        {
            if (!request.IsValidParams)
                return new BadRequestResponse(ResponseMessages.InvalidRequest);

            var skill = _mapper.Map<Skill>(request);

            await _repository.Skills.CreateSkillAsync(skill);
            await _repository.SaveAsync();

            var skillToReturn = _mapper.Map<SkillToReturnDto>(skill);
            return new ApiOkResponse<SkillToReturnDto>(skillToReturn);
        }

        public async Task<ApiBaseResponse> Update(Guid id, SkillRequest request)
        {
            if (!request.IsValidParams)
                return new BadRequestResponse(ResponseMessages.InvalidRequest);

            var skillToUpdate = await _repository.Skills.FindSkillAsync(id, true);
            if (skillToUpdate == null)
                return new NotFoundResponse(ResponseMessages.SkillNotFound);

            skillToUpdate.Description = request.Description;
            skillToUpdate.UpdatedAt = DateTime.Now;

            _repository.Skills.UpdateSkill(skillToUpdate);
            await _repository.SaveAsync();

            return new ApiOkResponse<string>(ResponseMessages.SkillUpdated);
        }

        public async Task<ApiBaseResponse> Delete(Guid id)
        {
            var skillToDelete = await _repository.Skills.FindSkillAsync(id, true);
            if (skillToDelete == null)
                return new NotFoundResponse(ResponseMessages.SkillNotFound);

            _repository.Skills.DeleteSkill(skillToDelete);
            await _repository.SaveAsync();

            return new ApiOkResponse<string>(ResponseMessages.SkillDeleted);
        }

        public async Task<ApiBaseResponse> Get()
        {
            return new ApiOkResponse<IEnumerable<Skill>>(await _repository.Skills.FindSkills(false).ToListAsync());
        }

        public async Task<ApiBaseResponse> Get(Guid id)
        {
            Skill? skill = await _repository.Skills.FindSkillAsync(id, true);
            if (skill == null)
                return new NotFoundResponse(ResponseMessages.SkillNotFound);

            return new ApiOkResponse<Skill>(skill);
        }
    }
}
