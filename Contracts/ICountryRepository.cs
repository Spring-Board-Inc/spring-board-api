using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts
{
    public interface ICountryRepository
    {
        Task<PagedList<Country>> GetCountriesAsync(SearchParameters searchParameters, bool trackChanges);
        Task<Country?> GetCountryAsync(Guid id, bool trackChanges);
        Task CreateCountryAsync(Country country);
        void DeleteCountry(Country country);
        void UpdateCountry(Country country);
        IQueryable<Country> GetCountries(bool trackChanges);
    }
}
