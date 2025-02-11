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
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(MongoDbSettings settings) : base(settings)
        {}

        public async Task AddAsync(Company company) => 
            await CreateAsync(company);

        public async Task DeleteAsync(Expression<Func<Company, bool>> expression) => 
            await RemoveAsync(expression);

        public async Task EditAsync(Expression<Func<Company, bool>> expression, Company company) => 
            await UpdateAsync(expression, company);

        public async Task<Company?> FindAsync(Guid id) =>
            await GetAsync(c => c.Id.Equals(id));

        public PagedList<Company> FindAsync(SearchParameters parameters)
        {
            var companies = GetAsQueryable(_ => true)
                    .Search(parameters.SearchBy)
                    .OrderBy(c => c.Name)
                    .ThenBy(c => c.CreatedAt);

            return PagedList<Company>.ToPagedList(companies, parameters.PageNumber, parameters.PageSize);
        }

        public PagedList<Company> FindAsync(SearchParameters parameters, Guid userId, bool isEmployer = false)
        {
            var companies = isEmployer ?
                                 GetAsQueryable(c => c.UserId.Equals(userId))
                                    .OrderBy(c => c.Name)
                                    .ThenBy(c => c.CreatedAt)
                                    .Search(parameters.SearchBy) :
                                GetAsQueryable(_ => true)
                                    .OrderBy(c => c.Name)
                                    .ThenBy(c => c.CreatedAt)
                                    .Search(parameters.SearchBy);

            return PagedList<Company>.ToPagedList(companies, parameters.PageNumber, parameters.PageSize);
        }

        public IQueryable<Company> FindAsQueryable(Expression<Func<Company, bool>> expression) =>
            GetAsQueryable(expression);

        public async Task<long> Count(Expression<Func<Company, bool>> expression)
        {
            return await CountAsync(expression);
        }
    }
}
