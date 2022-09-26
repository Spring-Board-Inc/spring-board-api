using Entities.Models;

namespace Contracts
{
    public interface ICompanyRepository
    {
        Task CreateCompanyAsync(Company company);
        void DeleteCompany(Company company);
        void UpdateCompany(Company company);
        Task<Company?> FindCompanyAsync(Guid id, bool trackChanges);
        Task<IEnumerable<Company>> FindCompaniesAsync(bool trackChanges);
        IQueryable<Company> FindCompanies(bool trackChanges);
    }
}
