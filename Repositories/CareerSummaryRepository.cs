using Contracts;
using Entities.Models;
using Microsoft.Extensions.Options;
using Repositories.Configurations;
using System.Linq.Expressions;

namespace Repositories
{
    public class CareerSummaryRepository : MongoRepositoryBase<CareerSummary>, ICareerSummaryRepository
    {
        public CareerSummaryRepository(IOptions<MongoDbSettings> settings) : base(settings) {}

        public async Task AddAsync(CareerSummary entity) => 
            await CreateAsync(entity);

        public async Task DeleteAsync(Expression<Func<CareerSummary, bool>> expression) => 
            await RemoveAsync(expression);

        public async Task EditAsync(Expression<Func<CareerSummary, bool>> expression, CareerSummary entity) => 
            await UpdateAsync(expression, entity);

        public IQueryable<CareerSummary> FindQueryable(Guid userId) =>
            GetAsQueryable(cs => cs.UserId.Equals(userId));

        public IQueryable<CareerSummary> FindByIdAsQueryable(Guid id, Guid userId) =>
            GetAsQueryable(cs => cs.Id.Equals(id) && cs.UserId.Equals(userId));

        public async Task<bool> Exists(Guid userId) => 
            await ExistsAsync(cs => cs.UserId.Equals(userId));
    }
}
