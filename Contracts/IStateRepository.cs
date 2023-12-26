using Entities.Models;
using Shared.RequestFeatures;
using System.Linq.Expressions;

namespace Contracts
{
    public interface IStateRepository
    {
        Task AddAsync(State state);
        Task DeleteAsync(Expression<Func<State, bool>> expression);
        IQueryable<State> FindByCountryAsQueryable(Guid countryId);
        Task<State?> FindAsync(Guid id);
        IQueryable<State> FindByIdAsQueryable(Guid id);
        PagedList<State> FindByCountryIdAsync(StateSearchParameters parameters);
        PagedList<State> FindStates(StateSearchParameters searchParameters);
        Task EditAsync(Expression<Func<State, bool>> expression, State state);
        IQueryable<State> FindAsQueryable(Expression<Func<State, bool>> expression);
    }
}
