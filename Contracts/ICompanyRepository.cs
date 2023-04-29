using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts
{
    public interface ICompanyRepository
    {
        Task CreateCompanyAsync(Company company);
        void DeleteCompany(Company company);
        void UpdateCompany(Company company);
        Task<Company?> FindCompanyAsync(Guid id, bool trackChanges);
        Task<PagedList<Company>> FindCompaniesAsync(SearchParameters parameters, bool trackChanges);
        Task<PagedList<Company>> FindCompaniesAsync(SearchParameters parameters, bool trackChanges, string userId = "", bool isEmployer = false);
        Task<int> Count(bool trackChanges);
    }
}
