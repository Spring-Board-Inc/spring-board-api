using Contracts;
using Entities.Models;

namespace Repositories
{
    public class CareerSummaryRepository : RepositoryBase<CareerSummary>, ICareerSummaryRepository
    {
        public CareerSummaryRepository(RepositoryContext context) : base(context) {}

        public async Task CreateCareerSummaryAsync(CareerSummary entity) => await Create(entity);

        public void DeleteCareerSummary(CareerSummary entity) => Delete(entity);

        public void UpdateCareerSummary(CareerSummary entity) => Update(entity);

        public IQueryable<CareerSummary> FindCareerSummary(string userId, bool trackChanges) =>
            FindByCondition(cs => cs.UserId.Equals(userId), trackChanges);

        public IQueryable<CareerSummary> FindCareerSummary(Guid id, string userId, bool trackChanges) =>
            FindByCondition(cs => cs.Id.Equals(id) && cs.UserId.Equals(userId), trackChanges);

        public async Task<bool> Exists(string userId) => 
            await ExistsAsync(cs => cs.UserId.Equals(userId));
    }
}
