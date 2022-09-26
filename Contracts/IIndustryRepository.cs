using Entities.Models;

namespace Contracts
{
    public interface IIndustryRepository
    {
        Task CreateIndustryAsync(Industry industry);
        void UpdateIndustry(Industry industry);
        void DeleteIndustry(Industry industry);
        Task<Industry?> FindIndustryAsync(Guid id, bool trackChanges);
        Task<IEnumerable<Industry?>> FindIndustriesAsync(bool trackChanges);
        IQueryable<Industry> FindIndustries(bool trackChanges);
    }
}
