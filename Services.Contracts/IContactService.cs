using Entities.Response;
using Shared.DataTransferObjects;

namespace Services.Contracts
{
    public interface IContactService
    {
        Task<ApiBaseResponse> Create(ContactForCreationDto request);
    }
}
