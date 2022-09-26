using Entities.Models;

namespace Contracts
{
    public interface IJobRepository
    {
        Task CreateJobAsync(Job job); 
        void UpdateJob(Job job);
        void DeleteJob(Job job);
        Task<Job?> FindJobAsync(Guid id, bool trackChanges);
        Task<IEnumerable<Job>> FindJobsAsync(bool trackChanges);
        IQueryable<Job> FindJobs(bool trackChanges);
    }
}
