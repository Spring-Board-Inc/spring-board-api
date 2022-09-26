using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class JobTypeRepository : RepositoryBase<JobType>, IJobTypeRepository
    {
        public JobTypeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {}

        public async Task CreateJobTypeAsync(JobType jobType) => await Create(jobType);

        public void UpdateJobType(JobType jobType) => Update(jobType);

        public void DeleteJobType(JobType jobType) => Delete(jobType);

        public async Task<JobType?> FindJobTypeAsync(Guid id, bool trackChanges) =>
            await FindByCondition(jt => jt.Id.Equals(id), trackChanges)
                    .FirstOrDefaultAsync();

        public async Task<IEnumerable<JobType>> FindJobTypesAsync(bool trackChanges) =>
            await FindAll(trackChanges)
                    .OrderByDescending(jt => jt.CreatedAt)
                    .ToListAsync();

        public IQueryable<JobType> FindJobTypes(bool trackChanges) =>
            FindAll(trackChanges)
                .OrderByDescending(jt => jt.CreatedAt);
    }
}
