using Entities.Response;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Services.Contracts
{
    public interface ISkillService
    {
        Task<ApiBaseResponse> Create(SkillRequest request);
        Task<ApiBaseResponse> Get(SearchParameters parameters);
        Task<ApiBaseResponse> Delete(Guid id);
        Task<ApiBaseResponse> Update(Guid id, SkillRequest request);
        Task<ApiBaseResponse> Get(Guid id);
        Task<IEnumerable<SkillDto>> GetAll();
    }
}