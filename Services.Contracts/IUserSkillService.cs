using Entities.Response;
using Shared.DataTransferObjects;

namespace Services.Contracts
{
    public interface IUserSkillService
    {
        Task<ApiBaseResponse> Create(Guid userInfoId, Guid skillId, UserSkillRequest request);
        Task<ApiBaseResponse> Delete(Guid userInfoId, Guid skillId);
        Task<ApiBaseResponse> Update(Guid userInfoId, Guid skillId, UserSkillRequest request);
        Task<ApiBaseResponse> Get(Guid userInfoId, Guid skillId);
        IEnumerable<UserSkillMinInfo> Get(Guid userInfoId);
    }
}
