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
            if(request == null)
                return new BadRequestResponse(ResponseMessages.InvalidRequest);

            var workExperience = _mapper.Map<WorkExperience>(request);
            workExperience.UserInformationId = userInfoId;

            await _repository.WorkExperience.CreateWorkExperienceAsync(workExperience);
            await _repository.SaveAsync();

            var experienceToReturn = _mapper.Map<WorkExperienceToReturnDto>(workExperience);
            return new ApiOkResponse<WorkExperienceToReturnDto>(experienceToReturn);
        }

        public async Task<ApiBaseResponse> Update(Guid id, WorkExperienceRequest request)
        {
            if (request == null)
                return new BadRequestResponse(ResponseMessages.InvalidRequest);

            var workExperienceForUpdate = await _repository.WorkExperience.FindWorkExperienceAsync(id, true);
            if (workExperienceForUpdate == null)
                return new NotFoundResponse(ResponseMessages.WorkExperienceNotFound);

            _mapper.Map(request, workExperienceForUpdate);
            workExperienceForUpdate.UpdatedAt = DateTime.Now;

            _repository.WorkExperience.UpdateWorkExperience(workExperienceForUpdate);
            await _repository.SaveAsync();

            return new ApiOkResponse<string>(ResponseMessages.WorkExperienceUpdated);
        }

        public async Task<ApiBaseResponse> Delete(Guid id)
        {
            var workExperienceForDeletion = await _repository.WorkExperience.FindWorkExperienceAsync(id, true);
            if (workExperienceForDeletion == null)
                return new NotFoundResponse(ResponseMessages.WorkExperienceNotFound);

            _repository.WorkExperience.DeleteWorkExperience(workExperienceForDeletion);
            await _repository.SaveAsync();

            return new ApiOkResponse<string>(ResponseMessages.WorkExperienceDeleted);
        }
    }
}