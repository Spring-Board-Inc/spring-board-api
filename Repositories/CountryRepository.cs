using Contracts;
using Entities.Models;
using Microsoft.Extensions.Options;
using Mongo.Common.MongoDB;
using Mongo.Common.Settings;
using Repositories.Extensions;
using Shared.RequestFeatures;
using System.Linq.Expressions;

namespace Repositories
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        public CountryRepository(MongoDbSettings settings) : base(settings){}

        public async Task AddAsync(Country country) => 
            await CreateAsync(country);

        public async Task DeleteAsync(Expression<Func<Country, bool>> expression) => 
            await RemoveAsync(expression);

        public async Task<Country?> FindAsync(Guid id) =>
            await GetAsync(c => c.Id.Equals(id));

        public IQueryable<Country> FindAsQueryable() =>
            GetAsQueryable(x => x.IsDeprecated == false)
                .OrderBy(c => c.Name);

        public PagedList<Country> FindAsync(SearchParameters searchParameters)
        {
            var endDate = searchParameters.EndDate == DateTime.MaxValue ? searchParameters.EndDate : searchParameters.EndDate.AddDays(1);
            var countries = GetAsQueryable(c => c.IsDeprecated == false && c.CreatedAt >= searchParameters.StartDate && c.CreatedAt <= endDate)
                            .OrderBy(c => c.Name)
                            .ThenByDescending(c => c.CreatedAt)
                            .Search(searchParameters.SearchBy);

            return PagedList<Country>.ToPagedList(countries, searchParameters.PageNumber, searchParameters.PageSize);
        }

        public async Task EditAsync(Expression<Func<Country, bool>> expression, Country country) => 
            await UpdateAsync(expression, country);
    }
}
