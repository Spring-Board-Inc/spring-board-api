using Entities.Models;

namespace Contracts
{
    public interface ITokenRepository
    {
        Task<Token?> GetToken(string token, bool trackChanges);
        void DeleteToken(Token token);
        void UpdateToken(Token token);
        Task CreateToken(Token token);
    }
}
