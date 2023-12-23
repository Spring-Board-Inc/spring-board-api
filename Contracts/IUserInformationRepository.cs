using Entities.Models;
using System.Linq.Expressions;

namespace Contracts
{
    public interface IUserInformationRepository
    {
        Task AddAsync(UserInformation userInformation);
        Task DeleteAsync(Expression<Func<UserInformation, bool>> expression);
        Task<UserInformation?> GetByUserIdAsync(Guid userId);
        Task<UserInformation?> GetByIdAsync(Guid id);
        Task EditAsync(Expression<Func<UserInformation, bool>> expression, UserInformation userInformation);
        Task<bool> ExistsAsync(Guid userId);
    }
}