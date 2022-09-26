using Entities.Response;
using Shared.DataTransferObjects;

namespace Services.Contracts
{
    public interface ISkillService
    {
        Task<ApiBaseResponse> Create(SkillRequest request);
        Task<ApiBaseResponse> Get();
        Task<ApiBaseResponse> Delete(Guid id);
        Task<ApiBaseResponse> Update(Guid id, SkillRequest request);
    }
}