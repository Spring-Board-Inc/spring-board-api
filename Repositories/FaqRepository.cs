using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Extensions;
using Shared.RequestFeatures;

namespace Repositories
{
    public class FaqRepository : RepositoryBase<Faq>, IFaqRepository
    {
        public FaqRepository(RepositoryContext context)
            : base(context) { }

        public async Task CreateAsync(Faq faq) => await Create(faq);
        public void UpdateFaq(Faq faq) => Update(faq);
        public void DeleteFaq(Faq faq) => Delete(faq);
        public async Task<Faq> GetAsync(Guid id, bool trackChanges) =>
            await FindByCondition(f => f.Id.Equals(id) && f.IsDeprecated == false, trackChanges)
                    .FirstOrDefaultAsync();

        public async Task<PagedList<Faq>> GetAsync(SearchParameters parameters)
        {
            var faqs = await FindByCondition(f => f.IsDeprecated == false, false)
                                .OrderBy(f => f.CreatedAt)
                                .Search(parameters.SearchBy)
                                .ToListAsync();

            return PagedList<Faq>.ToPagedList(faqs, parameters.PageNumber, parameters.PageSize);
        }
    }
}
