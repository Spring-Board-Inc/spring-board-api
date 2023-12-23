using Entities.Models;
using System.Linq.Expressions;

namespace Contracts
{
    public interface ICareerSummaryRepository
    {
        Task AddAsync(CareerSummary entity);
        Task DeleteAsync(Expression<Func<CareerSummary, bool>> expression);
        Task EditAsync(Expression<Func<CareerSummary, bool>> expression, CareerSummary entity);
        Task<bool> Exists(Guid userId);
        IQueryable<CareerSummary> FindByIdAsQueryable(Guid id, Guid userId);
        IQueryable<CareerSummary> FindQueryable(Guid userId);
    }
}
