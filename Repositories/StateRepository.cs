using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Repositories.Configurations;
using Repositories.Extensions;
using Shared.RequestFeatures;
using System.Linq.Expressions;

namespace Repositories
{
    public class StateRepository : MongoRepositoryBase<State>, IStateRepository
    {
        public StateRepository(IOptions<MongoDbSettings> settings) : base(settings)
        {}

        public async Task AddAsync(State state) => 
            await CreateAsync(state);

        public async Task DeleteAsync(Expression<Func<State, bool>> expression) => 
            await RemoveAsync(expression);

        public async Task<State?> FindAsync(Guid id) =>
            await GetAsync(s => s.Id.Equals(id));

        public IQueryable<State> FindByCountryAsQueryable(Guid countryId) =>
            GetAsQueryable(s => s.CountryId.Equals(countryId))
            .OrderBy(s => s.AdminArea);

        public IQueryable<State> FindByIdAsQueryable(Guid id) =>
            GetAsQueryable(s => s.Id.Equals(id))
                .Include(x => x.Country);

        public PagedList<State> FindByCountryIdAsync(StateSearchParameters parameters)
        {
            var states = GetAsQueryable(s =>
                    s.CountryId.Equals(parameters.CountryId) &&
                    (s.CreatedAt >= parameters.StartDate &&
                        s.CreatedAt <= (parameters.EndDate == DateTime.MaxValue ? parameters.EndDate : parameters.EndDate.AddDays(1))))
                .OrderBy(s => s.AdminArea)
                .ThenByDescending(s => s.CreatedAt)
                .Search(parameters.SearchBy)
                .Include(x => x.Country);         

            return PagedList<State>.ToPagedList(states, parameters.PageNumber, parameters.PageSize);
        }

        public PagedList<State> FindStates(StateSearchParameters searchParameters)
        {
            var endDate = searchParameters.EndDate == DateTime.MaxValue ? searchParameters.EndDate : searchParameters.EndDate.AddDays(1);
            var states = GetAsQueryable(s => s.CreatedAt >= searchParameters.StartDate && s.CreatedAt <= endDate)
                .Include(s => s.Country)
                .OrderByDescending(s => s.CreatedAt)
                .ThenBy(s => s.AdminArea)
                .Search(searchParameters.SearchBy);

            return PagedList<State>.ToPagedList(states, searchParameters.PageNumber, searchParameters.PageSize);
        }

        public async Task EditAsync(Expression<Func<State, bool>> expression, State state) => 
            await UpdateAsync(expression, state);
    }
}
