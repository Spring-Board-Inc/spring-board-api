using Entities.Response;

namespace Services.Contracts
{
    public interface IUserInformationService
    {
        Task<bool> Exists(Guid userId);
        Task<ApiBaseResponse> Create(Guid userId);
        Task<ApiBaseResponse> Get(Guid userId);
        Task<ApiBaseResponse> Delete(Guid id);
    }
}
