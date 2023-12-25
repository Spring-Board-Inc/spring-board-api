using Entities.Response;
using Shared.DataTransferObjects;

namespace Services.Contracts
{
    public interface IWorkExperienceService
    {
        Task<ApiBaseResponse> Create(Guid userInfoId, WorkExperienceRequest request);
        Task<ApiBaseResponse> Update(Guid id, WorkExperienceRequest request);
        Task<ApiBaseResponse> Delete(Guid id);
        Task<ApiBaseResponse> Get(Guid id);
        IEnumerable<WorkExperienceMinInfo> GetAsList(Guid userInfoId);
    }
}