using Entities.Response;
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
        ApiBaseResponse Get(SearchParameters searchParameters);
        ApiBaseResponse Get(JobSearchParams searchParams);
        Task<ApiBaseResponse> Apply(Guid jobId, string applicantId, CvToSendDto dto);
        ApiBaseResponse Get(Guid companyId, SearchParameters searchParams);
        ApiBaseResponse Get(string userId, SearchParameters searchParams);
        Task<ApiBaseResponse> GetApplicants(Guid jobId, SearchParameters searchParams);
        Task<ApiBaseResponse> JobStats();
        Task<ApiBaseResponse> GetApplicant(Guid jobId, Guid applicantId);
    }
}