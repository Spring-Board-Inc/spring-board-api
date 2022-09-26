using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class TokenRepository : RepositoryBase<Token>, ITokenRepository
    {
        public TokenRepository(RepositoryContext repositoryContext) 
            : base(repositoryContext){}

        public async Task CreateToken(Token token) => await Create(token);

        public void UpdateToken(Token token) => Update(token);
        public void DeleteToken(Token token) => Delete(token);

        public async Task<Token?> GetToken(string token, bool trackChanges) => 
            await FindByCondition(t => t.Value == token, trackChanges).FirstOrDefaultAsync();
    }
}