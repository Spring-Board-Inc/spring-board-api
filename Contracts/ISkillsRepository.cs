using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts
{
    public interface ISkillsRepository
    {
        PagedList<Skill> FindSkills(SearchParameters parameters, bool trackChanges);
        Task<IEnumerable<Skill>> FindSkillsAsync(bool trackChanges);
        Task<Skill?> FindSkillAsync(Guid id, bool trackChanges);
        void UpdateSkill(Skill skill);
        void DeleteSkill(Skill skill);
        Task CreateSkillAsync(Skill skill);
    }
}
