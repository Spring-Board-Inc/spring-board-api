using Entities.Models;

namespace Contracts
{
    public interface IJobTypeRepository
    {
        Task CreateJobTypeAsync(JobType jobType);
        void UpdateJobType(JobType jobType);
        void DeleteJobType(JobType jobType);
        Task<JobType?> FindJobTypeAsync(Guid id, bool trackChanges);
        Task<IEnumerable<JobType>> FindJobTypesAsync(bool trackChanges);
        IQueryable<JobType> FindJobTypes(bool trackChanges);
    }
}
