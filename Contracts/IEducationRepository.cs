using Entities.Models;
using System.Linq.Expressions;

namespace Contracts
{
    public interface IEducationRepository
    {
        Task AddAsync(Education education);
        Task DeleteAsync(Expression<Func<Education, bool>> expression);
        Task EditAsync(Expression<Func<Education, bool>> expression, Education education);
        IQueryable<Education> FindAsQueryable(Expression<Func<Education, bool>> expression);
        Task<Education?> FindByIdAsync(Guid id);
        IEnumerable<Education> FindByUserInfoId(Guid userInfoId);
        IQueryable<Education> FindByUserInfoIdAsQueryable(Guid userInfoId);
    }
}
