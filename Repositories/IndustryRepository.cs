using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Extensions;
using Shared.RequestFeatures;

namespace Repositories
{
    public class IndustryRepository : RepositoryBase<Industry>, IIndustryRepository
    {
        public IndustryRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {}

        public async Task CreateIndustryAsync(Industry industry) => await Create(industry);

        public void UpdateIndustry(Industry industry) => Update(industry);

        public void DeleteIndustry(Industry industry) => Delete(industry);

        public async Task<Industry?> FindIndustryAsync(Guid id, bool trackChanges) =>
            await FindByCondition(i => i.Id.Equals(id), trackChanges)
                    .FirstOrDefaultAsync();

        public async Task<PagedList<Industry>> FindIndustriesAsync(SearchParameters searchTerms, bool trackChanges)
        {
            var industries = await FindByCondition(i => i.IsDeprecated == false, trackChanges)
                    .Search(searchTerms.SearchBy)
                    .OrderBy(i => i.Name)
                    .ToListAsync();

            return PagedList<Industry>.ToPagedList(industries, searchTerms.PageNumber, searchTerms.PageSize);
        }

        public IQueryable<Industry> FindIndustries(bool trackChanges) =>
            FindAll(trackChanges)
                .OrderBy(i => i.Name);
    }
}