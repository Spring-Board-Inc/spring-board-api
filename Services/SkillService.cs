using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.Response;
using Services.Contracts;
using Shared.DataTransferObjects;
using Shared.Helpers;
using Shared.RequestFeatures;

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
            await _repository.Skills.AddAsync(skill);

            var skillToReturn = _mapper.Map<SkillToReturnDto>(skill);
            return new ApiOkResponse<SkillToReturnDto>(skillToReturn);
        }

        public async Task<ApiBaseResponse> Update(Guid id, SkillRequest request)
        {
            if (!request.IsValidParams)
                return new BadRequestResponse(ResponseMessages.InvalidRequest);

            var skillToUpdate = await _repository.Skills.FindByIdAsync(id);
            if (skillToUpdate == null)
                return new NotFoundResponse(ResponseMessages.SkillNotFound);

            skillToUpdate.Description = request.Description;
            skillToUpdate.UpdatedAt = DateTime.UtcNow;
            await _repository.Skills.EditAsync(x => x.Id.Equals(id), skillToUpdate);

            return new ApiOkResponse<bool>(true);
        }

        public async Task<ApiBaseResponse> Delete(Guid id)
        {
            var skillToDelete = await _repository.Skills.FindByIdAsync(id);
            if (skillToDelete == null)
                return new NotFoundResponse(ResponseMessages.SkillNotFound);

            await _repository.Skills.DeleteAsync(x => x.Id.Equals(id));
            return new ApiOkResponse<bool>(true);
        }

        public ApiBaseResponse Get(SearchParameters parameters)
        {
            var skills = _repository.Skills.Find(parameters);
            var data = _mapper.Map<IEnumerable<SkillDto>>(skills);

            var paged = PaginatedListDto<SkillDto>.Paginate(data, skills.MetaData);
            return new ApiOkResponse<PaginatedListDto<SkillDto>>(paged);
        }

        public async Task<List<SkillDto>> GetAll()
        {
            var result =  await _repository.Skills.Find();
            return _mapper.Map<List<SkillDto>>(result.OrderBy(x => x.Description).ToList());
        }
           

        public async Task<ApiBaseResponse> Get(Guid id)
        {
            Skill? skill = await _repository.Skills.FindByIdAsync(id);
            if (skill == null)
                return new NotFoundResponse(ResponseMessages.SkillNotFound);

            return new ApiOkResponse<Skill>(skill);
        }
    }
}
