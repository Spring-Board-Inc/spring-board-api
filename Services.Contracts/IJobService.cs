using Entities.Response;
using Shared.DataTransferObjects;

namespace Services.Contracts
{
    public interface IJobService
    {
        Task<ApiBaseResponse> Create(JobRequestObject request);
    }
}
