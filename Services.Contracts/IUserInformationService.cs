using Entities.Response;
using Shared.DataTransferObjects;

namespace Services.Contracts
{
    public interface IUserInformationService
    {
        Task<bool> Exists(string userId);
        Task<ApiBaseResponse> Create(string userId, UserInformationDto dto);
        Task<ApiBaseResponse> Get(string userId);
        Task<ApiBaseResponse> Update(Guid id, UserInformationDto dto);
        Task<ApiBaseResponse> Delete(Guid id);
    }
}
