using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Extensions;
using Shared.RequestFeatures;

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

        public async Task<PagedList<Job>> FindJobs(SearchParameters parameters)
        {
            var endDate = parameters.EndDate == DateTime.MaxValue ? parameters.EndDate : parameters.EndDate.AddDays(1);
            var jobs = await FindByCondition(j => j.IsDeprecated == false &&   
                                    (j.CreatedAt >= parameters.StartDate && j.CreatedAt <= endDate), false)
                                .Search(parameters.SearchBy)
                                .Include(j => j.Industry)
                                .Include(j => j.Company)
                                .Include(j => j.State)
                                .Include(j => j.Country)
                                .Include(j => j.Type)
                                .OrderByDescending(j => j.CreatedAt)
                                .ToListAsync();

            return PagedList<Job>.ToPagedList(jobs, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<PagedList<Job>> FindJobs(SearchParameters parameters, bool trackChanges)
        {
            var jobs = await FindByCondition(j => j.IsDeprecated == false, trackChanges)
                                .Search(parameters.SearchBy)
                                .Include(j => j.Industry)
                                .Include(j => j.Company)
                                .Include(j => j.State)
                                .Include(j => j.Country)
                                .Include(j => j.Type)
                                .OrderByDescending(j => j.CreatedAt)
                                .ToListAsync();

            return PagedList<Job>.ToPagedList(jobs, parameters.PageNumber, parameters.PageSize);
        }
    }
}