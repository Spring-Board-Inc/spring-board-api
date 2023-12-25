using Entities.Models;
using Shared.RequestFeatures;
using System.Linq.Expressions;

namespace Contracts
{
    public interface IIndustryRepository
    {
        Task AddAsync(Industry industry);
        Task DeleteAsync(Expression<Func<Industry, bool>> expression);
        Task EditAsync(Expression<Func<Industry, bool>> expression, Industry industry);
        PagedList<Industry> Find(SearchParameters searchTerms);
        IQueryable<Industry> FindAsQueryable();
        Task<Industry?> FindAsync(Guid id);
    }
}
