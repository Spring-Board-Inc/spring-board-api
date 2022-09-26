using Entities.Models;
using Entities.Response;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Services.Contracts
{
    public interface ILocationService
    {
        Task<ApiBaseResponse> Get(SearchParameters locationParameters, bool trackChanges);
        Task<ApiBaseResponse> Get(Guid locationId, bool trackChanges);
        Task<ApiBaseResponse> Create(LocationForCreationDto location);
        Task<ApiBaseResponse> Delete(Guid locationId, bool trackChanges);
        Task<ApiBaseResponse> Update(Guid locationId, LocationForUpdateDto locationForUpdate, bool trackChanges);
    }
}
