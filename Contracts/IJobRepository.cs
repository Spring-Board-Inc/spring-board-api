using Entities.Models;
using Shared.RequestFeatures;
using System.Linq.Expressions;

namespace Contracts
{
    public interface IJobRepository
    {
        Task AddAsync(Job job);
        Task DeleteAsync(Expression<Func<Job, bool>> expression);
        Task EditAsync(Expression<Func<Job, bool>> expression, Job job);
        PagedList<Job> Find(SearchParameters parameters);
        IQueryable<Job> FindAsQueryable();
        Task<Job?> FindAsync(Guid id);
        IEnumerable<Job> FindAsync();
        PagedList<Job> FindNoDateFilter(SearchParameters parameters);
    }
}
