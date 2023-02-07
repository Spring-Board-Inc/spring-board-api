using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Extensions;
using Shared.RequestFeatures;

namespace Repositories
{
    public class StateRepository : RepositoryBase<State>, IStateRepository
    {
        public StateRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {}

        public async Task CreateStateAsync(State state) => await Create(state);

        public void DeleteState(State state) => Delete(state);

        public async Task<State?> GetStateAsync(Guid id, bool trackChanges) =>
            await FindByCondition(s => s.Id.Equals(id), trackChanges).FirstOrDefaultAsync();

        public IQueryable<State> GetStates(bool trackChanges) =>
            FindAll(trackChanges)
            .OrderByDescending(s => s.CreatedAt)
            .ThenBy(s => s.AdminArea);

        public IQueryable<State> GetState(Guid id, bool trackChanges = false) =>
            FindByCondition(s => s.Id.Equals(id), trackChanges)
                .Include(s => s.Country);

        public IQueryable<State> GetStatesByCountry(StateSearchParameters parameters, bool trackChanges = false) =>
            FindByCondition(s => 
                    s.CountryId.Equals(parameters.CountryId) &&
                    (s.CreatedAt >= parameters.StartDate && 
                        s.CreatedAt <= (parameters.EndDate == DateTime.MaxValue ? parameters.EndDate : parameters.EndDate.AddDays(1))), 
                trackChanges)
                .Search(parameters.SearchBy)
                .Include(s => s.Country)
                .OrderBy(s => s.AdminArea)
                .ThenByDescending(s => s.CreatedAt);

        public IQueryable<State> GetStates(StateSearchParameters searchParameters, bool trackChanges = false)
        {
            var endDate = searchParameters.EndDate == DateTime.MaxValue ? searchParameters.EndDate : searchParameters.EndDate.AddDays(1);
            return FindAll(trackChanges)
                .Where(s => s.CreatedAt >= searchParameters.StartDate && s.CreatedAt <= endDate)
                .Search(searchParameters.SearchBy)
                .Include(s => s.Country)
                .OrderByDescending(s => s.CreatedAt)
                .ThenBy(s => s.AdminArea);
        }

        public void UpdateState(State state) => Update(state);
    }
}
