using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts
{
    public interface IJobRepository
    {
        Task CreateJobAsync(Job job); 
        void UpdateJob(Job job);
        void DeleteJob(Job job);
        Task<Job?> FindJobAsync(Guid id, bool trackChanges);
        Task<IEnumerable<Job>> FindJobsAsync(bool trackChanges);
        Task<PagedList<Job>> FindJobs(SearchParameters parameters, bool trackChanges);
        Task<PagedList<Job>> FindJobs(SearchParameters parameters);
    }
}
