using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class UserInformationRepository : RepositoryBase<UserInformation>, IUserInformationRepository
    {
        public UserInformationRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {}

        public async Task CreateUserInformationAsync(UserInformation userInformation) => await Create(userInformation);
        public void UpdateUserInformation(UserInformation userInformation) => Update(userInformation);
        public void DeleteUserInformation(UserInformation userInformation) => Delete(userInformation);
        public async Task<UserInformation?> FindUserInformationAsync(string userId, bool trackChanges) =>
            await FindByCondition(ui => ui.UserId.Equals(userId), trackChanges)
                    .Include(ui => ui.Educations)
                    .Include(ui => ui.Certifications)
                    .Include(ui => ui.WorkExperiences)
                    .Include(ui => ui.UserSkills)
                    .FirstOrDefaultAsync();

        public async Task<UserInformation?> FindUserInformationAsync(Guid id, bool trackChanges) =>
            await FindByCondition(ui => ui.Id.Equals(id), trackChanges).FirstOrDefaultAsync();

        public async Task<bool> UserInformationExists(string userId) =>
            await ExistsAsync(ui => ui.UserId.Equals(userId));

        public IQueryable<UserInformation> FindUserInformation(string userId, bool trackChanges) =>
            FindByCondition(ui => ui.UserId.Equals(userId), trackChanges);
    }
}