using AutoMapper;
using Contracts;
using Entities.Enums;
using Entities.Models;
using Entities.Response;
using EnumsNET;
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
            if(!request.IsValidDateRange)
                return new BadRequestResponse(ResponseMessages.InvalidDateRange);

            if (!request.IsValidParams)
                return new BadRequestResponse(ResponseMessages.InvalidRequest);

            var levelOfEducation = ((ELevel)request.Level).AsString(EnumFormat.Description);
            var education = _mapper.Map<Education>(request);
            education.LevelOfEducation = levelOfEducation;
            education.UserInformationId = userInfoId;

            await _repositoryManager.Education.AddAsync(education);

            var educationToReturn = _mapper.Map<EducationToReturnDto>(education);
            return new ApiOkResponse<EducationToReturnDto>(educationToReturn);
        }

        public async Task<ApiBaseResponse> Get(Guid id)
        {
            var education = await _repositoryManager.Education.FindByIdAsync(id);
            if (education == null)
                return new NotFoundResponse(ResponseMessages.EducationNotFound);

            var educationForReturn = _mapper.Map<EducationToReturnDto>(education);
            return new ApiOkResponse<EducationToReturnDto>(educationForReturn);
        }

        public IEnumerable<EducationToReturnDto> GetByUserInfoId(Guid userInfoId)
        {
            var educations = _repositoryManager.Education.FindByUserInfoId(userInfoId);
            return _mapper.Map<IEnumerable<EducationToReturnDto>>(educations);
        }

        public async Task<ApiBaseResponse> Update(Guid id, EducationForUpdateDto request)
        {
            if (!request.IsValidDateRange)
                return new BadRequestResponse(ResponseMessages.InvalidDateRange);

            if (!request.IsValidParams)
                return new BadRequestResponse(ResponseMessages.InvalidRequest);

            var educationForUpdate = await _repositoryManager.Education.FindByIdAsync(id);
            if (educationForUpdate == null)
                return new NotFoundResponse(ResponseMessages.EducationNotFound);

            _mapper.Map(request, educationForUpdate);
            educationForUpdate.LevelOfEducation = ((ELevel)request.Level).AsString(EnumFormat.Description);
            educationForUpdate.UpdatedAt = DateTime.UtcNow;

            await _repositoryManager.Education.EditAsync(x => x.Id.Equals(id), educationForUpdate);
            return new ApiOkResponse<string>(ResponseMessages.EducationUpdated);
        }

        public async Task<ApiBaseResponse> Delete(Guid id)
        {
            var education = await _repositoryManager.Education.FindByIdAsync(id);
            if (education == null)
                return new NotFoundResponse(ResponseMessages.EducationNotFound);

            await _repositoryManager.Education.DeleteAsync(x => x.Id.Equals(id));
            return new ApiOkResponse<string>(ResponseMessages.EducationDeleted);
        }
    }
}
