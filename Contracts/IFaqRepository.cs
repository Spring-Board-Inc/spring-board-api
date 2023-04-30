using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts
{
    public interface IFaqRepository
    {
        Task CreateAsync(Faq faq);
        void DeleteFaq(Faq faq);
        Task<Faq> GetAsync(Guid id, bool trackChanges);
        Task<PagedList<Faq>> GetAsync(SearchParameters parameters);
        void UpdateFaq(Faq faq);
    }
}
