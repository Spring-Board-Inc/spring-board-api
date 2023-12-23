using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Repositories.Configurations;
using System.Linq.Expressions;

namespace Repositories
{
    public class ContactRepository : MongoRepositoryBase<Contact>, IContactRepository
    {
        public ContactRepository(IOptions<MongoDbSettings> settings)
            : base(settings)
        {}

        public async Task AddAsync(Contact contact) => 
            await CreateAsync(contact);

        public async Task EditAsync(Expression<Func<Contact, bool>> expression, Contact contact) => 
            await UpdateAsync(expression, contact);

        public async Task DeleteAsync(Expression<Func<Contact, bool>> expression) => 
            await RemoveAsync(expression);

        public async Task<Contact> FindAsync(Guid id) =>
            await GetAsQueryable(c => c.Id.Equals(id) && c.IsDeprecated == false)
                    .Include(c => c.Address)
                    .Include(c => c.Phones)
                    .Include(c => c.Email)
                    .FirstOrDefaultAsync();

        public async Task<Contact> FindAsync() =>
            await GetAsQueryable(c => c.IsDeprecated == false)
                    .Include(c => c.Address)
                    .Include(c => c.Phones)
                    .Include(c => c.Email)
                    .FirstOrDefaultAsync();

        public async Task<bool> Exists() =>
            await ExistsAsync(c => c.IsDeprecated == false);
    }
}
