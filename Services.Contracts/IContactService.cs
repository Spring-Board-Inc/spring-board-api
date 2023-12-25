using Entities.Response;
using Shared.DataTransferObjects;

namespace Services.Contracts
{
    public interface IContactService
    {
        Task<ApiBaseResponse> Create(ContactForCreationDto request);
        Task<ApiBaseResponse> Delete(Guid id);
        Task<ApiBaseResponse> Deprecate(Guid id);
        ApiBaseResponse Get();
        ApiBaseResponse Get(Guid id);
        Task<ApiBaseResponse> Update(Guid id, ContactForUpdateDto request);
    }
}
