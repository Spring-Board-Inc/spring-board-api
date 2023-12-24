using Contracts;
using Entities.Models;
using Microsoft.Extensions.Options;
using Repositories.Configurations;
using System.Linq.Expressions;

namespace Repositories
{
    public class JobTypeRepository : MongoRepositoryBase<JobType>, IJobTypeRepository
    {
        public JobTypeRepository(IOptions<MongoDbSettings> settings) : base(settings)
        {}

        public async Task AddAsync(JobType jobType) => 
            await CreateAsync(jobType);

        public async Task EditAsync(Expression<Func<JobType, bool>> expression, JobType jobType) => 
            await UpdateAsync(expression, jobType);

        public async Task DeleteAsync(Expression<Func<JobType, bool>> expression) => 
            await RemoveAsync(expression);

        public async Task<JobType?> FindAsync(Guid id) =>
            await GetAsync(jt => jt.Id.Equals(id));

        public IEnumerable<JobType> FindAsList() =>
            GetAsQueryable(x => x.IsDeprecated == false)
                    .OrderByDescending(jt => jt.CreatedAt)
                    .ToList();

        public IQueryable<JobType> FindAsQueryable() =>
            GetAsQueryable(x => x.IsDeprecated == false)
                .OrderByDescending(jt => jt.CreatedAt);
    }
}
