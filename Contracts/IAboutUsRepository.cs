using Entities.Models;

namespace Contracts
{
    public interface IAboutUsRepository
    {
        Task CreateAsync(AboutUs aboutUs);
        void UpdateAbout(AboutUs aboutUs);
        void DeleteAbout(AboutUs aboutUs);
        Task<AboutUs> Get(bool trackChanges);
        Task<AboutUs> Get(Guid id, bool trackChanges);
        Task<bool> Exists();
    }
}
