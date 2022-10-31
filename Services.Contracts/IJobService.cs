using Entities.Response;
using Microsoft.AspNetCore.Http;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Services.Contracts
{
    public interface IJobService
    {
        Task<ApiBaseResponse> Create(JobRequestObject request);
        Task<ApiBaseResponse> Update(Guid id, JobRequestObject request);
        Task<ApiBaseResponse> Delete(Guid id);
        Task<ApiBaseResponse> Get(Guid id);
        Task<ApiBaseResponse> Get(SearchParameters searchParameters);
        Task<ApiBaseResponse> Get(JobSearchParams searchParams);
        Task<ApiBaseResponse> Apply(Guid jobId, string applicantId, IFormFile cv);
        Task<ApiBaseResponse> Get(Guid companyId, SearchParameters searchParams);
        Task<ApiBaseResponse> Get(string userId, SearchParameters searchParams);
        Task<ApiBaseResponse> GetApplicants(Guid jobId, SearchParameters searchParams);
        Task<ApiBaseResponse> JobStats();
    }
}