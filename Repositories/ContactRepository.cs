using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class ContactRepository : RepositoryBase<Contact>, IContactRepository
    {
        public ContactRepository(RepositoryContext context)
            : base(context)
        {}

        public async Task CreateAsync(Contact contact) => await Create(contact);

        public void UpdateContact(Contact contact) => Update(contact);

        public void DeleteContact(Contact contact) => Delete(contact);

        public async Task<Contact> GetAsync(Guid id, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(id), trackChanges)
                    .Include(c => c.Address)
                    .Include(c => c.Phones)
                    .Include(c => c.Email)
                    .FirstOrDefaultAsync();

        public async Task<Contact> GetAsync(bool trackChanges) =>
            await FindByCondition(c => c.IsDeprecated == false, trackChanges)
                    .Include(c => c.Address)
                    .Include(c => c.Phones)
                    .Include(c => c.Email)
                    .FirstOrDefaultAsync();

        public async Task<bool> Exists() =>
            await ExistsAsync(c => c.IsDeprecated == false);
    }
}
