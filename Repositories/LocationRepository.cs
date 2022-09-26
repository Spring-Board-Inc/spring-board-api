using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Extensions;
using Shared.RequestFeatures;

namespace Repositories
{
    public class LocationRepository : RepositoryBase<Location>, ILocationRepository
    {
        public LocationRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        { }

        public async Task CreateLocationAsync(Location location) => await Create(location);

        public void DeleteLocation(Location location) => Delete(location);

        public void UpdateLocation(Location location) => Update(location);

        public async Task<IEnumerable<Location>> GetLocationsAsync(SearchParameters locationParameters, bool trackChanges)
        {
            var endDate = locationParameters.EndDate == DateTime.MaxValue ? locationParameters.EndDate : locationParameters.EndDate.AddDays(1);
            return await FindAll(trackChanges)
                                .Where(l => l.CreatedAt >= locationParameters.StartDate && l.CreatedAt <= endDate)
                                .SearchLocation(locationParameters.SearchBy)
                                .OrderByDescending(l => l.CreatedAt)
                                .ThenBy(l => l.State)
                                .ToListAsync();
        }

        public IQueryable<Location> GetLocations(bool trackChanges) =>
            FindAll(trackChanges)
                .OrderByDescending(l => l.CreatedAt)
                .ThenBy(l => l.Town);

        public async Task<Location?> GetLocationAsync(Guid locationId, bool trackChanges) =>
            await FindByCondition(l => l.Id.Equals(locationId), trackChanges)
                        .FirstOrDefaultAsync();
    }
}
