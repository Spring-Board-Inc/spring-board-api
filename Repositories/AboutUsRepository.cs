using Contracts;
using Entities.Models;
using Microsoft.Extensions.Options;
using Mongo.Common.MongoDB;
using Mongo.Common.Settings;
using System.Linq.Expressions;

namespace Repositories
{
    public class AboutUsRepository: Repository<AboutUs>, IAboutUsRepository
    {
        public AboutUsRepository(IOptions<MongoDbSettings> options)
            : base(options)
        {}

        public async Task AddAsync(AboutUs aboutUs) => 
            await CreateAsync(aboutUs);

        public async Task EditAsync(Expression<Func<AboutUs, bool>> expression, AboutUs aboutUs) => 
            await UpdateAsync(expression, aboutUs);

        public async Task DeleteAsync(Expression<Func<AboutUs, bool>> expression) => 
            await RemoveAsync(expression);

        public async Task<AboutUs> FindFirstAsync(Expression<Func<AboutUs, bool>> expression) =>
            await GetAsync(expression);

        public async Task<IEnumerable<AboutUs>> FindAllAsync() =>
            await GetAsync();

        public async Task<AboutUs> FindByIdAsync(Guid id) =>
            await GetAsync(a => a.Id.Equals(id) && a.IsDeprecated == false);

        public async Task<bool> Exists() =>
            await ExistsAsync(a => a.IsDeprecated == false);
    }
}
