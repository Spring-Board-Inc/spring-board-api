using Entities.Models;

namespace Contracts
{
    public interface IUserInformationRepository
    {
        Task<UserInformation?> FindUserInformationAsync(string userId, bool trackChanges);
        void DeleteUserInformation(UserInformation userInformation);
        void UpdateUserInformation(UserInformation userInformation);
        Task CreateUserInformationAsync(UserInformation userInformation);
        Task<bool> UserInformationExists(string userId);
        Task<UserInformation?> FindUserInformationAsync(Guid id, bool trackChanges);
        IQueryable<UserInformation> FindUserInformation(string userId, bool trackChanges);
        IQueryable<UserInformation> FindUserInformation(bool trackChanges);
    }
}