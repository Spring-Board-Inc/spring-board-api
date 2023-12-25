using Entities.Response;
using Shared.DataTransferObjects;

namespace Services.Contracts
{
    public interface IEducationService
    {
        Task<ApiBaseResponse> Create(Guid userInfoId, EducationForCreationDto request);
        Task<ApiBaseResponse> Delete(Guid id);
        Task<ApiBaseResponse> Update(Guid id, EducationForUpdateDto request);
        Task<ApiBaseResponse> Get(Guid id);
        IEnumerable<EducationToReturnDto> GetByUserInfoId(Guid userInfoId);
    }
}
