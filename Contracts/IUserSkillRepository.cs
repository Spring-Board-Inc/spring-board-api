using Entities.Models;
using System.Linq.Expressions;

namespace Contracts
{
    public interface IUserSkillRepository
    {
        Task AddAsync(UserSkill userSkill);
        Task DeleteAsync(Expression<Func<UserSkill, bool>> expression);
        Task EditAsync(Expression<Func<UserSkill, bool>> expression, UserSkill userSkill);
        Task<bool> Exists(Expression<Func<UserSkill, bool>> expression);
        IEnumerable<UserSkill> FindAsList(Guid userInfoId);
        IQueryable<UserSkill> FindAsQueryable(Guid userInfoId);
        IQueryable<UserSkill> FindAsQueryable(Expression<Func<UserSkill, bool>> expression);
        Task<UserSkill?> FindAsync(Guid userInfoId, Guid skillId);
    }
}
