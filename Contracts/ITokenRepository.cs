using Entities.Models;
using System.Linq.Expressions;

namespace Contracts
{
    public interface ITokenRepository
    {
        Task CreateToken(Token token);
        Task DeleteToken(Expression<Func<Token, bool>> expression);
        Task<Token?> GetToken(string token);
        Task UpdateToken(Expression<Func<Token, bool>> expression, Token token);
    }
}
