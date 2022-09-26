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
    public class EducationService : IEducationService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public EducationService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<ApiBaseResponse> Create(Guid userInfoId, EducationForCreationDto request)
        {
            var levelOfEducation = ((ELevel)request.Level).ToString();
            var education = _mapper.Map<Education>(request);
            education.LevelOfEducation = levelOfEducation;
            education.UserInformationId = userInfoId;

            await _repositoryManager.Education.CreateEducationAsync(education);
            await _repositoryManager.SaveAsync();

            var educationToReturn = _mapper.Map<EducationToReturnDto>(education);
            return new ApiOkResponse<EducationToReturnDto>(educationToReturn);
        }

        public async Task<ApiBaseResponse> Get(Guid id)
        {
            var education = await _repositoryManager.Education.GetEducationAsync(id, false);
            if (education == null)
                return new NotFoundResponse(ResponseMessages.EducationNotFound);

            var educationForReturn = _mapper.Map<EducationToReturnDto>(education);
            return new ApiOkResponse<EducationToReturnDto>(educationForReturn);
        }

        public async Task<IEnumerable<EducationToReturnDto>> Get(Guid id, bool track)
        {
            var educations = await _repositoryManager.Education.GetEducationsAsync(id, track);
            return _mapper.Map<IEnumerable<EducationToReturnDto>>(educations);
        }

        public async Task<ApiBaseResponse> Update(Guid id, EducationForUpdateDto request)
        {
            var educationForUpdate = await _repositoryManager.Education.GetEducationAsync(id, true);
            if (educationForUpdate == null)
                return new NotFoundResponse(ResponseMessages.EducationNotFound);

            _mapper.Map(request, educationForUpdate);
            educationForUpdate.LevelOfEducation = ((ELevel)request.Level).ToString();
            educationForUpdate.UpdatedAt = DateTime.Now;

            _repositoryManager.Education.UpdateEducation(educationForUpdate);
            await _repositoryManager.SaveAsync();

            return new ApiOkResponse<string>(ResponseMessages.EducationUpdated);
        }

        public async Task<ApiBaseResponse> Delete(Guid id)
        {
            var education = await _repositoryManager.Education.GetEducationAsync(id, true);
            if (education == null)
                return new NotFoundResponse(ResponseMessages.EducationNotFound);

            _repositoryManager.Education.DeleteEducation(education);
            await _repositoryManager.SaveAsync();

            return new ApiOkResponse<string>(ResponseMessages.EducationDeleted);
        }
    }
}
