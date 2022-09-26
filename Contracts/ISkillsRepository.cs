using Entities.Models;

namespace Contracts
{
    public interface ISkillsRepository
    {
        IQueryable<Skill> FindSkills(bool trackChanges);
        Task<IEnumerable<Skill>> FindSkillsAsync(bool trackChanges);
        Task<Skill?> FindSkillAsync(Guid id, bool trackChanges);
        void UpdateSkill(Skill skill);
        void DeleteSkill(Skill skill);
        Task CreateSkillAsync(Skill skill);
    }
}
