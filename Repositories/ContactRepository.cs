using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Mongo.Common.MongoDB;
using Mongo.Common.Settings;
using System.Linq.Expressions;

namespace Repositories
{
    public class ContactRepository : Repository<Contact>, IContactRepository
    {
        public ContactRepository(MongoDbSettings settings)
            : base(settings)
        {}

        public async Task AddAsync(Contact contact) => 
            await CreateAsync(contact);

        public async Task EditAsync(Expression<Func<Contact, bool>> expression, Contact contact) => 
            await UpdateAsync(expression, contact);

        public async Task DeleteAsync(Expression<Func<Contact, bool>> expression) => 
            await RemoveAsync(expression);

        public Contact FindAsync(Guid id) =>
            GetAsQueryable(c => c.Id.Equals(id) && c.IsDeprecated == false)
                    .Include(c => c.Address)
                    .Include(c => c.Phones)
                    .Include(c => c.Email)
                    .FirstOrDefault();

        public Contact FindAsync() =>
            GetAsQueryable(c => c.IsDeprecated == false)
                    .Include(c => c.Address)
                    .Include(c => c.Phones)
                    .Include(c => c.Email)
                    .FirstOrDefault();

        public async Task<bool> Exists() =>
            await ExistsAsync(c => c.IsDeprecated == false);
    }
}
