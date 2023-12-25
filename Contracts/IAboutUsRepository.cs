using Entities.Models;
using System.Linq.Expressions;

namespace Contracts
{
    public interface IAboutUsRepository
    {
        Task AddAsync(AboutUs aboutUs);
        Task DeleteAsync(Expression<Func<AboutUs, bool>> expression);
        Task<bool> Exists();
        Task<IEnumerable<AboutUs>> FindAllAsync();
        Task<AboutUs> FindByIdAsync(Guid id);
        Task<AboutUs> FindFirstAsync(Expression<Func<AboutUs, bool>> expression);
        Task EditAsync(Expression<Func<AboutUs, bool>> expression, AboutUs aboutUs);
    }
}
