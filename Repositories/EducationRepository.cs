using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class EducationRepository : RepositoryBase<Education>, IEducationRepository
    {
        public EducationRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {}

        public async Task CreateEducationAsync(Education education) => await Create(education);

        public void UpdateEducation(Education education) => Update(education);

        public void DeleteEducation(Education education) => Delete(education);
        public async Task<Education?> GetEducationAsync(Guid id, bool trackChanges) =>
            await FindByCondition(ed => ed.Id.Equals(id), trackChanges).FirstOrDefaultAsync();

        public async Task<IEnumerable<Education>> GetEducationsAsync(Guid id, bool trackChanges) =>
            await FindByCondition(ed => ed.UserInformationId.Equals(id), trackChanges)
                      .OrderByDescending(ed => ed.StartDate)
                      .ToListAsync();

        public IQueryable<Education> GetEducations(Guid userInfoId, bool trackChanges) =>
            FindByCondition(ed => ed.UserInformationId.Equals(userInfoId), trackChanges)
                .OrderByDescending(ed => ed.StartDate);
    }
}