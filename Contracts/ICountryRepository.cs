using Entities.Models;
using Shared.RequestFeatures;
using System.Linq.Expressions;

namespace Contracts
{
    public interface ICountryRepository
    {
        IQueryable<Country> FindAsQueryable();

        Task AddAsync(Country country);
        Task DeleteAsync(Expression<Func<Country, bool>> expression);
        Task EditAsync(Expression<Func<Country, bool>> expression, Country country);
        Task<Country?> FindAsync(Guid id);
        PagedList<Country> FindAsync(SearchParameters searchParameters);
    }
}
