using Contracts;
using Entities.Models;
using Microsoft.Extensions.Options;
using Repositories.Configurations;
using System.Linq.Expressions;

namespace Repositories
{
    public class UserSkillRepository : MongoRepositoryBase<UserSkill>, IUserSkillRepository
    {
        public UserSkillRepository(IOptions<MongoDbSettings> options) : base(options)
        {}

        public async Task AddAsync(UserSkill userSkill) => 
            await CreateAsync(userSkill);

        public async Task EditAsync(Expression<Func<UserSkill, bool>> expression, UserSkill userSkill) => 
            await UpdateAsync(expression, userSkill);

        public async Task DeleteAsync(Expression<Func<UserSkill, bool>> expression) => 
            await RemoveAsync(expression);

        public async Task<UserSkill?> FindAsync(Guid userInfoId, Guid skillId) =>
            await GetAsync(us => us.UserInformationId.Equals(userInfoId) && us.SkillId.Equals(skillId));

        public IEnumerable<UserSkill> FindAsList(Guid userInfoId) =>
            GetAsQueryable(us => us.UserInformationId.Equals(userInfoId))
                .OrderByDescending(us => us.Skill)
                .ToList();

        public IQueryable<UserSkill> FindAsQueryable(Guid userInfoId) =>
            GetAsQueryable(us => us.UserInformationId.Equals(userInfoId))
                .OrderByDescending(us => us.Skill);
    }
}