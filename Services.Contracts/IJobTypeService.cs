using Entities.Models;
using Entities.Response;
using Shared.DataTransferObjects;

namespace Services.Contracts
{
    public interface IJobTypeService
    {
        Task<ApiBaseResponse> Create(JobTypeRequestObject request);
        Task<ApiBaseResponse> Delete(Guid id);
        Task<ApiBaseResponse> Update(Guid id, JobTypeRequestObject request);
        Task<IEnumerable<JobTypeToReturnDto>> Get();
        Task<ApiBaseResponse> Get(Guid id);
    }
}
