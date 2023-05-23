using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class AboutUsRepository: RepositoryBase<AboutUs>, IAboutUsRepository
    {
        public AboutUsRepository(RepositoryContext context)
            : base(context)
        {}

        public async Task CreateAsync(AboutUs aboutUs) => await Create(aboutUs);

        public void UpdateAbout(AboutUs aboutUs) => Update(aboutUs);

        public void DeleteAbout(AboutUs aboutUs) => Delete(aboutUs);

        public async Task<AboutUs> Get(bool trackChanges) =>
            await FindByCondition(a => a.IsDeprecated == false, trackChanges)
                    .FirstOrDefaultAsync();

        public async Task<IEnumerable<AboutUs>> GetAll(bool trackChanges) =>
            await FindAll(trackChanges)
                    .ToListAsync();

        public async Task<AboutUs> Get(Guid id, bool trackChanges) =>
            await FindByCondition(a => a.Id.Equals(id) && a.IsDeprecated == false, trackChanges)
                    .OrderByDescending(a => a.CreatedAt)
                    .FirstOrDefaultAsync();

        public async Task<bool> Exists() =>
            await ExistsAsync(a => a.IsDeprecated == false);
    }
}
