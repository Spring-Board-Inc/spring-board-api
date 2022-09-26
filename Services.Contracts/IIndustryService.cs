using Entities.Response;
using Shared.DataTransferObjects;

namespace Services.Contracts
{
    public interface IIndustryService
    {
        Task<ApiBaseResponse> Create(IndustryRequestObject request);
        Task<ApiBaseResponse> Delete(Guid id);
        Task<ApiBaseResponse> Update(Guid id, IndustryRequestObject request);
        Task<IEnumerable<IndustryToReturnDto>> Get();
        Task<ApiBaseResponse> Get(Guid id);
    }
}
