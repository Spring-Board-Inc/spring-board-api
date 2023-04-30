using Entities.Response;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Services.Contracts
{
    public interface IFaqService
    {
        Task<ApiBaseResponse> Create(FaqForCreationDto request);
        Task<ApiBaseResponse> Delete(Guid id);
        Task<ApiBaseResponse> Get(Guid id);
        Task<PaginatedListDto<FaqToReturnDto>> Get(SearchParameters parameters);
        Task<ApiBaseResponse> Update(Guid id, FaqForUpdateDto request);
    }
}
