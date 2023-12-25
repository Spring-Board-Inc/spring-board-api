using Entities.Models;
using System.Linq.Expressions;

namespace Contracts
{
    public interface IContactRepository
    {
        Task AddAsync(Contact contact);
        Task DeleteAsync(Expression<Func<Contact, bool>> expression);
        Task EditAsync(Expression<Func<Contact, bool>> expression, Contact contact);
        Task<bool> Exists();
        Contact FindAsync(Guid id);
        Contact FindAsync();
    }
}
