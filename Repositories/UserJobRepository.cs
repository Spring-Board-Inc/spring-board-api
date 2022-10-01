using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class UserJobRepository : RepositoryBase<UserJob>, IUserJobRepository
    {
        public UserJobRepository(RepositoryContext context) : base(context) { }

        public async Task CreateUserJob(UserJob userJob) => await Create(userJob);

        public void UpdateUserJob(UserJob userJob) => Update(userJob);

        public void DeleteUserJob(UserJob userJob) => Delete(userJob);

        public async Task<UserJob?> FindUserJobAsync(string userId, Guid jobId, bool trackChanges) =>
            await FindByCondition(uj => uj.UserId.Equals(userId) && uj.JobId.Equals(jobId), trackChanges)
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<UserJob>> FindUserJobsAsync(string userId, bool trackChanges) =>
            await FindByCondition(uj => uj.UserId.Equals(userId), trackChanges)
                .OrderByDescending(uj => uj.CreatedAt)
                .ToListAsync();

        public IQueryable<UserJob> FindUserJobs
            (string userId, bool trackChanges) =>
            FindByCondition(uj => uj.UserId.Equals(userId), trackChanges)
                .OrderByDescending(uj => uj.CreatedAt);

        public async Task<IEnumerable<UserJob>> FindUserJobsAsync(Guid jobId, bool trackChanges) =>
            await FindByCondition(uj => uj.JobId.Equals(jobId), trackChanges)
                .OrderByDescending(uj => uj.CreatedAt)
                .ToListAsync();

        public IQueryable<UserJob> FindUserJobs
            (Guid jobId, bool trackChanges) =>
            FindByCondition(uj => uj.JobId.Equals(jobId), trackChanges)
                .OrderByDescending(uj => uj.CreatedAt);

        public async Task<bool> Exists(string userId, Guid jobId) => 
            await ExistsAsync(uj => uj.UserId.Equals(userId) && uj.JobId.Equals(jobId));
    }
}
