using Entities.Models;

namespace Contracts
{
    public interface IContactRepository
    {
        Task CreateAsync(Contact contact);
        void UpdateContact(Contact contact);
        void DeleteContact(Contact contact);
        Task<Contact> GetAsync(Guid id, bool trackChanges);
        Task<Contact> GetAsync(bool trackChanges);
        Task<bool> Exists();
    }
}
