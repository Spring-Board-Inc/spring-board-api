using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class SkillsRepository : RepositoryBase<Skill>, ISkillsRepository
    {
        public SkillsRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {}

        public async Task CreateSkillAsync(Skill skill) => await Create(skill);

        public void DeleteSkill(Skill skill) => Delete(skill);

        public void UpdateSkill(Skill skill) => Update(skill);
        public async Task<Skill?> FindSkillAsync(Guid id, bool trackChanges) =>
            await FindByCondition(sk => sk.Id.Equals(id), trackChanges)
                    .FirstOrDefaultAsync();

        public async Task<IEnumerable<Skill>> FindSkillsAsync(bool trackChanges) =>
            await FindAll(trackChanges)
                    .ToListAsync();

        public IQueryable<Skill> FindSkills(bool trackChanges) =>
            FindAll(trackChanges)
                .OrderByDescending(sk => sk.Description);
    }
}