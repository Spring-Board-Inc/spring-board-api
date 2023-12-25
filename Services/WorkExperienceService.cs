using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.Response;
using Services.Contracts;
using Shared.DataTransferObjects;
using Shared.Helpers;

namespace Services
{
    public class WorkExperienceService : IWorkExperienceService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public WorkExperienceService
            (
                IRepositoryManager repository,
                IMapper mapper
            )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiBaseResponse> Create(Guid userInfoId, WorkExperienceRequest request)
        {
            if(!request.IsValidDateRange)
                return new BadRequestResponse(ResponseMessages.InvalidDateRange);

            if(!request.IsValidParams)
                return new BadRequestResponse(ResponseMessages.InvalidRequest);

            var workExperience = _mapper.Map<WorkExperience>(request);
            workExperience.UserInformationId = userInfoId;

            await _repository.WorkExperience.AddAsync(workExperience);

            var experienceToReturn = _mapper.Map<WorkExperienceToReturnDto>(workExperience);
            return new ApiOkResponse<WorkExperienceToReturnDto>(experienceToReturn);
        }

        public async Task<ApiBaseResponse> Get(Guid id)
        {
            var exp = await _repository.WorkExperience.FindAsync(id);
            if (exp == null)
                return new NotFoundResponse(ResponseMessages.WorkExperienceNotFound);

            var expForReturn = _mapper.Map<WorkExperienceMinInfo>(exp);
            return new ApiOkResponse<WorkExperienceMinInfo>(expForReturn);
        }

        public IEnumerable<WorkExperienceMinInfo> GetAsList(Guid userInfoId)
        {
            var exp = _repository.WorkExperience.FindByUserInfoId(userInfoId).ToList();
            return _mapper.Map<IEnumerable<WorkExperienceMinInfo>>(exp);
        }

        public async Task<ApiBaseResponse> Update(Guid id, WorkExperienceRequest request)
        {
            if (!request.IsValidDateRange)
                return new BadRequestResponse(ResponseMessages.InvalidDateRange);

            if (!request.IsValidParams)
                return new BadRequestResponse(ResponseMessages.InvalidRequest);

            var workExperienceForUpdate = await _repository.WorkExperience.FindAsync(id);
            if (workExperienceForUpdate == null)
                return new NotFoundResponse(ResponseMessages.WorkExperienceNotFound);

            _mapper.Map(request, workExperienceForUpdate);
            workExperienceForUpdate.UpdatedAt = DateTime.Now;

            await _repository.WorkExperience.EditAsync(x => x.Id.Equals(id), workExperienceForUpdate);
            return new ApiOkResponse<bool>(true);
        }

        public async Task<ApiBaseResponse> Delete(Guid id)
        {
            var workExperienceForDeletion = await _repository.WorkExperience.FindAsync(id);
            if (workExperienceForDeletion == null)
                return new NotFoundResponse(ResponseMessages.WorkExperienceNotFound);

            await _repository.WorkExperience.DeleteAsync(x => x.Id.Equals(id));
            return new ApiOkResponse<bool>(true);
        }
    }
}