﻿using Contracts;
using Entities.Models;
using Microsoft.Extensions.Options;
using Mongo.Common.MongoDB;
using Mongo.Common.Settings;
using Repositories.Extensions;
using Shared.RequestFeatures;
using System.Linq.Expressions;

namespace Repositories
{
    public class SkillsRepository : Repository<Skill>, ISkillsRepository
    {
        public SkillsRepository(MongoDbSettings mongoDbSettings) 
            : base(mongoDbSettings){}

        public async Task AddAsync(Skill skill) => 
            await CreateAsync(skill);

        public async Task DeleteAsync(Expression<Func<Skill, bool>> expression) =>
            await RemoveAsync(expression);

        public async Task<Skill?> FindByIdAsync(Guid id) =>
            await GetAsync(x => x.Id.Equals(id) && x.IsDeprecated == false);

        public PagedList<Skill> Find(SearchParameters parameters)
        {
            var skills = GetAsQueryable(x => x.IsDeprecated == false)
                .OrderBy(x => x.Description)
                .Search(parameters.SearchBy);

            return PagedList<Skill>.ToPagedList(skills, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<IEnumerable<Skill>> Find() =>
            await GetAsync();

        public async Task EditAsync(Expression<Func<Skill, bool>> expression, Skill skill) =>
            await UpdateAsync(expression, skill);

        public async Task<bool> Exists(Expression<Func<Skill, bool>> expression) =>
            await ExistsAsync(expression);
    }
}