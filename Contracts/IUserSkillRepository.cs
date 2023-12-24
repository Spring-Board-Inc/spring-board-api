using Entities.Models;
using System.Linq.Expressions;

namespace Contracts
{
    public interface IUserSkillRepository
    {
        Task AddAsync(UserSkill userSkill);
        Task DeleteAsync(Expression<Func<UserSkill, bool>> expression);
        Task EditAsync(Expression<Func<UserSkill, bool>> expression, UserSkill userSkill);
        IEnumerable<UserSkill> FindAsList(Guid userInfoId);
        IQueryable<UserSkill> FindAsQueryable(Guid userInfoId);
        Task<UserSkill?> FindAsync(Guid userInfoId, Guid skillId);
    }
}
