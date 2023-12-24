using Entities.Models;
using System.Linq.Expressions;

namespace Contracts
{
    public interface IUserJobRepository
    {
        Task AddAsync(UserJob userJob);
        Task DeleteAsync(Expression<Func<UserJob, bool>> expression);
        Task EditAsync(Expression<Func<UserJob, bool>> expression, UserJob userJob);
        Task<bool> Exists(string userId, Guid jobId);
        IQueryable<UserJob> FindAsQueryable(string userId);
        IQueryable<UserJob> FindAsQueryable(Guid jobId, Guid userId);
        Task<UserJob?> FindAsync(string userId, Guid jobId);
        IQueryable<UserJob> FindByJobIdAsQueryable(Guid jobId);
        IEnumerable<UserJob> FindByUserId(string userId);
    }
}
