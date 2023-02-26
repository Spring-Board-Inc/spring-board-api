using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<Company>> FindCompaniesAsync(bool trackChanges) =>
            await FindAll(trackChanges)
                    .OrderByDescending(c => c.Name)
                    .ThenBy(c => c.CreatedAt)
                    .ToListAsync();

        public IQueryable<Company> FindCompanies(bool trackChanges) => 
            FindAll(trackChanges)
                .OrderByDescending(c => c.CreatedAt);
    }
}
