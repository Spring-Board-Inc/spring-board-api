using Entities.Response;
using Shared.DataTransferObjects;

namespace Services.Contracts
{
    public interface ICompanyService
    {
        Task<ApiBaseResponse> Create(CompanyRequestObject request);
        Task<ApiBaseResponse> Delete(Guid id);
        Task<ApiBaseResponse> Update(Guid id, CompanyRequestObject request);
        Task<IEnumerable<CompanyToReturnDto>> Get();
        Task<ApiBaseResponse> Get(Guid id);
    }
}
