using Entities.Response;
using Shared;

namespace Services.Contracts
{
    public interface ICareerSummaryService
    {
        Task<ApiBaseResponse> Create(string userId, CareerSummaryDto request);
        ApiBaseResponse GetMany(string userId);
        ApiBaseResponse Get(string userId);
        Task<ApiBaseResponse> Update(Guid id, string userId, CareerSummaryUpdateDto request);
        Task<ApiBaseResponse> Delete(Guid id, string userId);
    }
}
