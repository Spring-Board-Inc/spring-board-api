using Entities.Response;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Services.Contracts
{
    public interface IIndustryService
    {
        Task<ApiBaseResponse> Create(IndustryRequestObject request);
        Task<ApiBaseResponse> Delete(Guid id);
        Task<ApiBaseResponse> Update(Guid id, IndustryRequestObject request);
        PaginatedListDto<IndustryToReturnDto> Get(SearchParameters parameters);
        Task<ApiBaseResponse> Get(Guid id);
        IEnumerable<IndustryToReturnDto> GetAll();
    }
}
