using Entities.Response;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Services.Contracts
{
    public interface ICompanyService
    {
        Task<ApiBaseResponse> Create(string userId, CompanyRequestObject request);
        Task<ApiBaseResponse> Delete(Guid id);
        Task<ApiBaseResponse> Update(Guid id, CompanyRequestObject request);
        ApiBaseResponse Get(string userId, bool isEmployer, SearchParameters parameters);
        Task<ApiBaseResponse> Get(Guid id);
    }
}
