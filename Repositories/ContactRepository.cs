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

        public void DeleteAsync(Contact contact) => Delete(contact);

        public async Task<Contact> GetAsync(Guid id, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(id), trackChanges)
                    .FirstOrDefaultAsync();

        public async Task<Contact> GetAsync(bool trackChanges) =>
            await FindByCondition(c => c.IsDeprecated == false, trackChanges)
                    .FirstOrDefaultAsync();

        public async Task<bool> Exists() =>
            await ExistsAsync(c => c.IsDeprecated == false);
    }
}
