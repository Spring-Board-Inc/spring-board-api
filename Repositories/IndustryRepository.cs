using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<Industry?>> FindIndustriesAsync(bool trackChanges) =>
            await FindAll(trackChanges)
                    .OrderByDescending(i => i.CreatedAt)
                    .ToListAsync();

        public IQueryable<Industry> FindIndustries(bool trackChanges) =>
            FindAll(trackChanges)
                .OrderByDescending(i => i.CreatedAt);
    }
}