using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts
{
    public interface ILocationRepository
    {
        Task<IEnumerable<Location>> GetLocationsAsync(SearchParameters locationParameters, bool trackChanges);
        Task<Location?> GetLocationAsync(Guid locationId, bool trackChanges);
        Task CreateLocationAsync(Location location);
        void DeleteLocation(Location location);
        IQueryable<Location> GetLocations(bool trackChanges);
    }
}