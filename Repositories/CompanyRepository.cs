using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Extensions;
using Shared.RequestFeatures;

namespace Repositories
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {}

        public async Task CreateCompanyAsync(Company company) => await Create(company);

        public void DeleteCompany(Company company) => Delete(company);

        public void UpdateCompany(Company company) => Update(company);

        public async Task<Company?> FindCompanyAsync(Guid id, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(id), trackChanges)
                    .FirstOrDefaultAsync();

        public async Task<PagedList<Company>> FindCompaniesAsync(SearchParameters parameters, bool trackChanges)
        {
            var companies = await FindAll(trackChanges)
                    .Search(parameters.SearchBy)
                    .OrderBy(c => c.Name)
                    .ThenBy(c => c.CreatedAt)
                    .ToListAsync();

            return PagedList<Company>.ToPagedList(companies, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<PagedList<Company>> FindCompaniesAsync(SearchParameters parameters, bool trackChanges, string userId = "", bool isEmployer = false)
        {
            var companies = isEmployer ?
                                await FindByCondition(c => c.UserId.Equals(userId), trackChanges)
                                        .Search(parameters.SearchBy)
                                        .OrderBy(c => c.Name)
                                        .ThenBy(c => c.CreatedAt)
                                        .ToListAsync() :
                                await FindAll(trackChanges)
                                    .Search(parameters.SearchBy)
                                    .OrderBy(c => c.Name)
                                    .ThenBy(c => c.CreatedAt)
                                    .ToListAsync();

            return PagedList<Company>.ToPagedList(companies, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<int> Count(bool trackChanges)
        {
            var companies = await FindByCondition(c => c.IsDeprecated == false, trackChanges)
                .ToListAsync();

            return companies.Count;
        }
    }
}
