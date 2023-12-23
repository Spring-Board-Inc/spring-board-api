using Entities.Models;
using Shared.RequestFeatures;
using System.Linq.Expressions;

namespace Contracts
{
    public interface IFaqRepository
    {
        Task AddAsync(Faq faq);
        Task DeleteAsync(Expression<Func<Faq, bool>> expression);
        Task EditAsync(Expression<Func<Faq, bool>> expression, Faq faq);
        Task<Faq> FindAsync(Guid id);
        PagedList<Faq> FindAsync(SearchParameters parameters);
    }
}
