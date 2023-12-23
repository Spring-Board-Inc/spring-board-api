using Contracts;
using Entities.Models;
using Microsoft.Extensions.Options;
using Repositories.Configurations;
using System.Linq.Expressions;

namespace Repositories
{
    public class EducationRepository : MongoRepositoryBase<Education>, IEducationRepository
    {
        public EducationRepository(IOptions<MongoDbSettings> settings) : base(settings)
        {}

        public async Task AddAsync(Education education) => 
            await CreateAsync(education);

        public async Task EditAsync(Expression<Func<Education, bool>> expression, Education education) => 
            await UpdateAsync(expression, education);

        public async Task DeleteAsync(Expression<Func<Education, bool>> expression) => 
            await RemoveAsync(expression);
        public async Task<Education?> FindByIdAsync(Guid id) =>
            await GetAsync(ed => ed.Id.Equals(id));

        public IEnumerable<Education> FindByUserInfoId(Guid userInfoId) =>
            GetAsQueryable(ed => ed.UserInformationId.Equals(userInfoId))
                      .OrderByDescending(ed => ed.StartDate)
                      .ToList();

        public IQueryable<Education> FindByUserInfoIdAsQueryable(Guid userInfoId) =>
            GetAsQueryable(ed => ed.UserInformationId.Equals(userInfoId))
                .OrderByDescending(ed => ed.StartDate);
    }
}