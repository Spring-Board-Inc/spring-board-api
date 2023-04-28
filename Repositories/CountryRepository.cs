using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Extensions;
using Shared.RequestFeatures;

namespace Repositories
{
    public class CountryRepository : RepositoryBase<Country>, ICountryRepository
    {
        public CountryRepository(RepositoryContext repositoryContext) : base(repositoryContext){}

        public async Task CreateCountryAsync(Country country) => await Create(country);

        public void DeleteCountry(Country country) => Delete(country);

        public async Task<Country?> GetCountryAsync(Guid id, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(id), trackChanges).FirstOrDefaultAsync();

        public IQueryable<Country> GetCountries(bool trackChanges) =>
            FindAll(trackChanges)
                .OrderBy(c => c.Name);

        public async Task<PagedList<Country>> GetCountriesAsync(SearchParameters searchParameters, bool trackChanges)
        {
            var endDate = searchParameters.EndDate == DateTime.MaxValue ? searchParameters.EndDate : searchParameters.EndDate.AddDays(1);
            var countries = await FindAll(trackChanges)
                            .Where(c => c.CreatedAt >= searchParameters.StartDate && c.CreatedAt <= endDate)
                            .OrderBy(c => c.Name)
                            .ThenByDescending(c => c.CreatedAt)
                            .Search(searchParameters.SearchBy)
                            .ToListAsync();

            return PagedList<Country>.ToPagedList(countries, searchParameters.PageNumber, searchParameters.PageSize);
        }

        public void UpdateCountry(Country country) => Update(country);
    }
}
