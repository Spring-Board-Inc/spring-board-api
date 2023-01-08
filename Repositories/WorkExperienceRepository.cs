using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class WorkExperienceRepository : RepositoryBase<WorkExperience>, IWorkExperienceRepository
    {
        public WorkExperienceRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {}

        public async Task CreateWorkExperienceAsync(WorkExperience workExperience) => await Create(workExperience);

        public void DeleteWorkExperience(WorkExperience workExperience) => Delete(workExperience);

        public void UpdateWorkExperience(WorkExperience workExperience) => Update(workExperience);

        public async Task<WorkExperience?> FindWorkExperienceAsync(Guid id, bool trackChanges) =>
            await FindByCondition(exp => exp.Id.Equals(id), trackChanges)
                    .FirstOrDefaultAsync();

        public async Task<IEnumerable<WorkExperience>> FindWorkExperiencesAsync(Guid userInfoId, bool trackChanges) =>
            await FindByCondition(exp => exp.UserInformationId.Equals(userInfoId), trackChanges)
                    .ToListAsync();
        public IQueryable<WorkExperience> FindExperiences(Guid id) =>
            FindByCondition(exp => exp.Id.Equals(id), false)
                .OrderByDescending(exp => exp.StartDate);

        public IQueryable<WorkExperience> FindExperiences(Guid userInfoId, bool trackChanges) =>
            FindByCondition(exp => exp.UserInformationId.Equals(userInfoId), trackChanges)
                .OrderByDescending(exp => exp.StartDate);
    }
}
