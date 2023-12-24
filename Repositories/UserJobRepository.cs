using Contracts;
using Entities.Models;
using Microsoft.Extensions.Options;
using Repositories.Configurations;
using System.Linq.Expressions;

namespace Repositories
{
    public class UserJobRepository : MongoRepositoryBase<UserJob>, IUserJobRepository
    {
        public UserJobRepository(IOptions<MongoDbSettings> options) : base(options) { }

        public async Task AddAsync(UserJob userJob) => 
            await CreateAsync(userJob);

        public async Task EditAsync(Expression<Func<UserJob, bool>> expression, UserJob userJob) => 
            await UpdateAsync(expression, userJob);

        public async Task DeleteAsync(Expression<Func<UserJob, bool>> expression) => 
            await RemoveAsync(expression);

        public async Task<UserJob?> FindAsync(string userId, Guid jobId) =>
            await GetAsync(uj => uj.UserId.Equals(userId) && uj.JobId.Equals(jobId));

        public IEnumerable<UserJob> FindByUserId(string userId) =>
            GetAsQueryable(uj => uj.UserId.Equals(userId))
                .OrderByDescending(uj => uj.CreatedAt)
                .ToList();

        public IQueryable<UserJob> FindAsQueryable(string userId) =>
            GetAsQueryable(uj => uj.UserId.Equals(userId))
                .OrderByDescending(uj => uj.CreatedAt);

        public IEnumerable<UserJob> FindByJobId(Guid jobId) =>
            GetAsQueryable(uj => uj.JobId.Equals(jobId))
                .OrderByDescending(uj => uj.CreatedAt)
                .ToList();

        public IQueryable<UserJob> FindByJobIdAsQueryable(Guid jobId) =>
            GetAsQueryable(uj => uj.JobId.Equals(jobId))
                .OrderByDescending(uj => uj.CreatedAt);

        public IQueryable<UserJob> FindAsQueryable(Guid jobId, Guid userId) =>
            GetAsQueryable(uj => uj.JobId.Equals(jobId) && userId.Equals(userId));

        public async Task<bool> Exists(string userId, Guid jobId) => 
            await ExistsAsync(uj => uj.UserId.Equals(userId) && uj.JobId.Equals(jobId));
    }
}
