using Entities.Models;

namespace Contracts
{
    public interface IUserSkillRepository
    {
        Task CreateUserSkill(UserSkill userSkill);
        void UpdateUserSkill(UserSkill userSkill);
        void DeleteUserSkill(UserSkill userSkill);
        Task<UserSkill?> FindUserSkillAsync(Guid userInfoId, Guid skillId, bool trackChanges);
        Task<IEnumerable<UserSkill>> FindUserSkillsAsync(Guid userInfoId, bool trackChanges);
        IQueryable<UserSkill> FindUserSkills(Guid userInfoId, bool trackChanges);
    }
}
