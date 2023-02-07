using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts
{
    public interface IStateRepository
    {
        IQueryable<State> GetStates(StateSearchParameters searchParameters, bool trackChanges);
        Task<State?> GetStateAsync(Guid id, bool trackChanges);
        Task CreateStateAsync(State state);
        void DeleteState(State state);
        void UpdateState(State state);
        IQueryable<State> GetStates(bool trackChanges);
        IQueryable<State> GetState(Guid id, bool trackChanges);
        IQueryable<State> GetStatesByCountry(StateSearchParameters parameters, bool trackChanges);
    }
}
