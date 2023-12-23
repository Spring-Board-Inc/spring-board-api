using Entities.Models;
using Shared.RequestFeatures;
using System.Linq.Expressions;

namespace Contracts
{
    public interface ICompanyRepository
    {
        Task AddAsync(Company company);
        Task<long> Count(Expression<Func<Company, bool>> expression);
        Task DeleteAsync(Expression<Func<Company, bool>> expression);
        Task EditAsync(Expression<Func<Company, bool>> expression, Company company);
        Task<Company?> FindAsync(Guid id);
        PagedList<Company> FindAsync(SearchParameters parameters, Guid userId, bool isEmployer = false);
    }
}
