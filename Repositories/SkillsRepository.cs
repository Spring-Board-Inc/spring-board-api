using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Repositories.Configurations;
using Repositories.Extensions;
using Shared.RequestFeatures;

namespace Repositories
{
    public class SkillsRepository : MongoRepositoryBase<Skill>, ISkillsRepository
    {
        public SkillsRepository(IOptions<MongoDbSettings> mongoDbSettings) 
            : base(mongoDbSettings){}

        public async Task CreateSkillAsync(Skill skill) => 
            await CreateAsync(skill);

        public void DeleteSkill(Skill skill)
        {
            throw new NotImplementedException();
        }

        public Task<Skill?> FindSkillAsync(Guid id, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        //public async Task CreateSkillAsync(Skill skill) => await Create(skill);

        //public void DeleteSkill(Skill skill) => Delete(skill);

        //public void UpdateSkill(Skill skill) => Update(skill);
        //public async Task<Skill?> FindSkillAsync(Guid id, bool trackChanges) =>
        //    await FindByCondition(sk => sk.Id.Equals(id), trackChanges)
        //            .FirstOrDefaultAsync();

        //public async Task<IEnumerable<Skill>> FindSkillsAsync(bool trackChanges) =>
        //    await FindAll(trackChanges)
        //            .OrderBy(s => s.Description)
        //            .ToListAsync();

        //public async Task<PagedList<Skill>> FindSkills(SearchParameters parameters, bool trackChanges)
        //{
        //    var skills = await FindByCondition(s => s.IsDeprecated == false, trackChanges)
        //                        .OrderBy(sk => sk.Description)
        //                        .Search(parameters.SearchBy)
        //                        .ToListAsync();

        //    return PagedList<Skill>.ToPagedList(skills, parameters.PageNumber, parameters.PageSize);
        //}

        public PagedList<Skill> FindSkills(SearchParameters parameters, bool trackChanges)
        {
            var skills = GetAsQueryable(x => x.IsDeprecated == false)
                .OrderBy(x => x.Description)
                .Search(parameters.SearchBy);

            return PagedList<Skill>.ToPagedList(skills, parameters.PageNumber, parameters.PageSize);
        }

        public Task<IEnumerable<Skill>> FindSkillsAsync(bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public void UpdateSkill(Skill skill)
        {
            throw new NotImplementedException();
        }
    }
}