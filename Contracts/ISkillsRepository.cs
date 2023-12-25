using Entities.Models;
using Shared.RequestFeatures;
using System.Linq.Expressions;

namespace Contracts
{
    public interface ISkillsRepository
    {
        Task AddAsync(Skill skill);
        Task DeleteAsync(Expression<Func<Skill, bool>> expression);
        PagedList<Skill> Find(SearchParameters parameters);
        Task<IEnumerable<Skill>> Find();
        Task<Skill?> FindByIdAsync(Guid id);
        Task EditAsync(Expression<Func<Skill, bool>> expression, Skill skill);
        Task<bool> Exists(Expression<Func<Skill, bool>> expression);
    }
}
