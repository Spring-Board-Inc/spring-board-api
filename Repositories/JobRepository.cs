using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class JobRepository : RepositoryBase<Job>, IJobRepository
    {
        public JobRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {}

        public async Task CreateJobAsync(Job job) => await Create(job);

        public void UpdateJob(Job job) => Update(job);

        public void DeleteJob(Job job) => Delete(job);

        public async Task<Job?> FindJobAsync(Guid id, bool trackChanges) =>
            await FindByCondition(j => j.Id.Equals(id), trackChanges)
                    .Include(j => j.Company)
                    .Include(j => j.Industry)
                    .Include(j => j.Type)
                    .Include(j => j.State)
                    .Include(j => j.Country)
                    .FirstOrDefaultAsync();

        public async Task<IEnumerable<Job>> FindJobsAsync(bool trackChanges) =>
            await FindAll(trackChanges)
                    .OrderByDescending(j => j.CreatedAt)
                    .ThenBy(j => j.Title)
                    .ToListAsync();

        public IQueryable<Job> FindJobs(bool trackChanges) =>
            FindAll(trackChanges)
               .OrderByDescending(j => j.CreatedAt)
               .ThenBy(j => j.Title);
    }
}