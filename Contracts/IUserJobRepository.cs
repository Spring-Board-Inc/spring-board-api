using Entities.Models;

namespace Contracts
{
    public interface IUserJobRepository
    {
        Task CreateUserJob(UserJob userJob);
        void UpdateUserJob(UserJob userJob);
        void DeleteUserJob(UserJob userJob);
        Task<UserJob?> FindUserJobAsync(string userId, Guid jobId, bool trackChanges);
        Task<IEnumerable<UserJob>> FindUserJobsAsync(string userId, bool trackChanges);
        IQueryable<UserJob> FindUserJobs(string userId, bool trackChanges);
        Task<IEnumerable<UserJob>> FindUserJobsAsync(Guid jobId, bool trackChanges);
        IQueryable<UserJob> FindUserJobs(Guid jobId, bool trackChanges);
        Task<bool> Exists(string userId, Guid jobId);
        IQueryable<UserJob> FindUserJob(Guid jobId, Guid userId, bool trackChanges);
    }
}
