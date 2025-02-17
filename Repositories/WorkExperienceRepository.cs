﻿using Contracts;
using Entities.Models;
using Microsoft.Extensions.Options;
using Mongo.Common.MongoDB;
using Mongo.Common.Settings;
using System.Linq.Expressions;

namespace Repositories
{
    public class WorkExperienceRepository : Repository<WorkExperience>, IWorkExperienceRepository
    {
        public WorkExperienceRepository(MongoDbSettings options) : base(options)
        {}

        public async Task AddAsync(WorkExperience workExperience) => 
            await CreateAsync(workExperience);

        public async Task DeleteAsync(Expression<Func<WorkExperience, bool>> expression) => 
            await RemoveAsync(expression);

        public async Task EditAsync(Expression<Func<WorkExperience, bool>> expression, WorkExperience workExperience) => 
            await UpdateAsync(expression, workExperience);

        public async Task<WorkExperience?> FindAsync(Guid id) =>
            await GetAsync(exp => exp.Id.Equals(id));

        public IEnumerable<WorkExperience> FindByUserInfoId(Guid userInfoId) =>
            GetAsQueryable(exp => exp.UserInformationId.Equals(userInfoId))
                    .ToList();
        public IQueryable<WorkExperience> FindByIdAsQueryable(Guid id) =>
            GetAsQueryable(exp => exp.Id.Equals(id))
                .OrderByDescending(exp => exp.StartDate);

        public IQueryable<WorkExperience> FindByUserInfoIdAsQueryable(Guid userInfoId) =>
            GetAsQueryable(exp => exp.UserInformationId.Equals(userInfoId))
                .OrderByDescending(exp => exp.StartDate);

        public IQueryable<WorkExperience> FindAsQueryable(Expression<Func<WorkExperience, bool>> expression) =>
            GetAsQueryable(expression);
    }
}
