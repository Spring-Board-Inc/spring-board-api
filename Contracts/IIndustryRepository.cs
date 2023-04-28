using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts
{
    public interface IIndustryRepository
    {
        Task CreateIndustryAsync(Industry industry);
        void UpdateIndustry(Industry industry);
        void DeleteIndustry(Industry industry);
        Task<Industry?> FindIndustryAsync(Guid id, bool trackChanges);
        Task<PagedList<Industry>> FindIndustriesAsync(SearchParameters searchTerms, bool trackChanges);
        IQueryable<Industry> FindIndustries(bool trackChanges);
    }
}
