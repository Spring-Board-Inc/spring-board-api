using Contracts;
using Entities.Models;
using Microsoft.Extensions.Options;
using Mongo.Common.MongoDB;
using Mongo.Common.Settings;
using Repositories.Extensions;
using Shared.RequestFeatures;
using System.Linq.Expressions;

namespace Repositories
{
    public class FaqRepository : Repository<Faq>, IFaqRepository
    {
        public FaqRepository(MongoDbSettings options)
            : base(options) { }

        public async Task AddAsync(Faq faq) => 
            await CreateAsync(faq);
        public async Task EditAsync(Expression<Func<Faq, bool>> expression, Faq faq) => 
            await UpdateAsync(expression, faq);
        public async Task DeleteAsync(Expression<Func<Faq, bool>> expression) => 
            await RemoveAsync(expression);
        public async Task<Faq> FindAsync(Guid id) =>
            await GetAsync(f => f.Id.Equals(id) && f.IsDeprecated == false);

        public PagedList<Faq> FindAsync(SearchParameters parameters)
        {
            var faqs = GetAsQueryable(f => f.IsDeprecated == false)
                                .OrderBy(f => f.CreatedAt)
                                .Search(parameters.SearchBy);

            return PagedList<Faq>.ToPagedList(faqs, parameters.PageNumber, parameters.PageSize);
        }
    }
}
