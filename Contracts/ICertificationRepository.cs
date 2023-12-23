using Entities.Models;
using System.Linq.Expressions;

namespace Contracts
{
    public interface ICertificationRepository
    {
        Task AddAsync(Certification certification);
        Task DeleteAsync(Expression<Func<Certification, bool>> expression);
        Task EditAsync(Expression<Func<Certification, bool>> expression, Certification certification);
        Task<Certification?> FindByIdAsync(Guid id);
        IQueryable<Certification> FindByUserInfoIdAsQueryable(Guid userInfoId);
        Task<List<Certification>> FindByUserInfoIdAsync(Guid userInfoId);
    }
}
