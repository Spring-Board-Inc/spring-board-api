using Contracts;
using Entities.Models;
using Microsoft.Extensions.Options;
using Mongo.Common.MongoDB;
using Mongo.Common.Settings;
using Repositories.Extensions;
using Shared.RequestFeatures;
using System.Linq.Expressions;

namespace Repositories
{
    public class JobRepository : Repository<Job>, IJobRepository
    {
        public JobRepository(MongoDbSettings options) : base(options)
        {}

        public async Task AddAsync(Job job) => 
            await CreateAsync(job);

        public async Task EditAsync(Expression<Func<Job, bool>> expression, Job job) => 
            await UpdateAsync(expression, job);

        public async Task DeleteAsync(Expression<Func<Job, bool>> expression) => 
            await RemoveAsync(expression);

        public Job? FindAsync(Guid id) =>
            GetAsQueryable(j => j.Id.Equals(id))
                    .FirstOrDefault();

        public IEnumerable<Job> FindAsync() =>
            GetAsQueryable(x => x.IsDeprecated == false)
                    .OrderByDescending(j => j.CreatedAt)
                    .ThenBy(j => j.Title)
                    .ToList();

        public IQueryable<Job> FindAsQueryable() =>
            GetAsQueryable(x => x.IsDeprecated == false)
               .OrderByDescending(j => j.CreatedAt)
               .ThenBy(j => j.Title);

        public PagedList<Job> Find(SearchParameters parameters)
        {
            var endDate = parameters.EndDate == DateTime.MaxValue ? parameters.EndDate : parameters.EndDate.AddDays(1);
            var jobs = GetAsQueryable(j => j.IsDeprecated == false && (j.CreatedAt >= parameters.StartDate && j.CreatedAt <= endDate))
                                .OrderByDescending(j => j.CreatedAt)
                                .Search(parameters.SearchBy);

            return PagedList<Job>.ToPagedList(jobs, parameters.PageNumber, parameters.PageSize);
        }

        public PagedList<Job> FindNoDateFilter(SearchParameters parameters)
        {
            var jobs = GetAsQueryable(j => j.IsDeprecated == false)
                                .OrderByDescending(j => j.CreatedAt)
                                .Search(parameters.SearchBy);

            return PagedList<Job>.ToPagedList(jobs, parameters.PageNumber, parameters.PageSize);
        }
    }
}