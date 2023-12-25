using Entities.Models;
using System.Linq.Expressions;

namespace Contracts
{
    public interface IJobTypeRepository
    {
        Task AddAsync(JobType jobType);
        Task DeleteAsync(Expression<Func<JobType, bool>> expression);
        Task EditAsync(Expression<Func<JobType, bool>> expression, JobType jobType);
        IQueryable<JobType> FindAsQueryable();
        IEnumerable<JobType> FindAsList();
        Task<JobType?> FindAsync(Guid id);
    }
}
