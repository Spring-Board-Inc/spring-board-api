﻿using Contracts;
using Entities.Models;
using Microsoft.Extensions.Options;
using Repositories.Configurations;
using System.Linq.Expressions;

namespace Repositories
{
    public class UserInformationRepository : MongoRepositoryBase<UserInformation>, IUserInformationRepository
    {
        public UserInformationRepository(IOptions<MongoDbSettings> options) : base(options)
        {}

        public async Task AddAsync(UserInformation userInformation) => await CreateAsync(userInformation);
        public async Task EditAsync(Expression<Func<UserInformation, bool>> expression, UserInformation userInformation) => 
            await base.UpdateAsync(expression, userInformation);
        public async Task DeleteAsync(Expression<Func<UserInformation, bool>> expression) => 
            await RemoveAsync(expression);
        public async Task<UserInformation?> GetByUserIdAsync(Guid userId) =>
            await GetAsync(ui => ui.UserId.Equals(userId));

        public async Task<UserInformation?> GetByIdAsync(Guid id) =>
            await GetAsync(ui => ui.Id == id);

        public async Task<bool> ExistsAsync(Guid userId) =>
            await ExistsAsync(ui => ui.UserId.Equals(userId));
    }
}