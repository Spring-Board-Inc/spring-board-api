using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class UserSkillRepository : RepositoryBase<UserSkill>, IUserSkillRepository
    {
        public UserSkillRepository(RepositoryContext context) : base(context)
        {}

        public async Task CreateUserSkill(UserSkill userSkill) => await Create(userSkill);

        public void UpdateUserSkill(UserSkill userSkill) => Update(userSkill);

        public void DeleteUserSkill(UserSkill userSkill) => Delete(userSkill);

        public async Task<UserSkill?> FindUserSkillAsync(Guid userInfoId, Guid skillId, bool trackChanges) =>
            await FindByCondition(us => us.UserInformationId.Equals(userInfoId) && us.SkillId.Equals(skillId), trackChanges)
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<UserSkill>> FindUserSkillsAsync(Guid userInfoId, bool trackChanges) =>
            await FindByCondition(us => us.UserInformationId.Equals(userInfoId), trackChanges)
                .OrderByDescending(us => us.Skill)
                .ToListAsync();

        public IQueryable<UserSkill> FindUserSkills(Guid userInfoId, bool trackChanges) =>
            FindByCondition(us => us.UserInformationId.Equals(userInfoId), trackChanges)
                .OrderByDescending(us => us.Skill);
    }
}