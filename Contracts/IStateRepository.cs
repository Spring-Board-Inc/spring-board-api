using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts
{
    public interface IStateRepository
    {
        Task<PagedList<State>> GetStates(StateSearchParameters searchParameters, bool trackChanges = false);
        Task<State?> GetStateAsync(Guid id, bool trackChanges);
        Task CreateStateAsync(State state);
        void DeleteState(State state);
        void UpdateState(State state);
        IQueryable<State> GetStates(Guid countryId, bool trackChanges);
        IQueryable<State> GetState(Guid id, bool trackChanges);
        Task<PagedList<State>> GetStatesByCountry(StateSearchParameters parameters, bool trackChanges = false);
    }
}
