using Contracts;
using Entities.Models;
using Microsoft.Extensions.Options;
using Mongo.Common.MongoDB;
using Mongo.Common.Settings;
using System.Linq.Expressions;

namespace Repositories
{
    public class TokenRepository : Repository<Token>, ITokenRepository
    {
        public TokenRepository(MongoDbSettings settings) 
            : base(settings){}

        public async Task CreateToken(Token token) => await CreateAsync(token);

        public async Task UpdateToken(Expression<Func<Token, bool>> expression, Token token) => 
            await UpdateAsync(expression, token);
        public async Task DeleteToken(Expression<Func<Token, bool>> expression) => 
            await RemoveAsync(expression);

        public async Task<Token?> GetToken(string token) => 
            await GetAsync(t => t.Value == token);
    }
}