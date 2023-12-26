using Entities.Models;
using System.Linq.Expressions;

namespace Contracts
{
    public interface IWorkExperienceRepository
    {
        Task DeleteAsync(Expression<Func<WorkExperience, bool>> expression);
        Task EditAsync(Expression<Func<WorkExperience, bool>> expression, WorkExperience workExperience);
        IQueryable<WorkExperience> FindByIdAsQueryable(Guid id);
        Task<WorkExperience?> FindAsync(Guid id);
        IEnumerable<WorkExperience> FindByUserInfoId(Guid userInfoId);
        Task AddAsync(WorkExperience workExperience);
        IQueryable<WorkExperience> FindByUserInfoIdAsQueryable(Guid userInfoId);
        IQueryable<WorkExperience> FindAsQueryable(Expression<Func<WorkExperience, bool>> expression);
    }
}