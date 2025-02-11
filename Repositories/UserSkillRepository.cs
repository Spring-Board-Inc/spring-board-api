using Contracts;
using Entities.Models;
using Microsoft.Extensions.Options;
using Mongo.Common.MongoDB;
using Mongo.Common.Settings;
using System.Linq.Expressions;

namespace Repositories
{
    public class UserSkillRepository : Repository<UserSkill>, IUserSkillRepository
    {
        public UserSkillRepository(MongoDbSettings options) : base(options)
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

        public IQueryable<UserSkill> FindAsQueryable(Expression<Func<UserSkill, bool>> expression) =>
            GetAsQueryable(expression);

        public async Task<bool> Exists(Expression<Func<UserSkill, bool>> expression) =>
            await ExistsAsync(expression);
    }
}