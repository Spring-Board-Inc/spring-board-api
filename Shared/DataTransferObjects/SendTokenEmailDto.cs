using Entities.Enums;
using Entities.Models;
using Microsoft.Extensions.Primitives;

namespace Shared.DataTransferObjects
{
    public class SendTokenEmailDto
    {
        public AppUser User { get; set; }
        public string Subject { get; set; }
        public StringValues Origin { get; set; }
        public EToken TokenType { get; set; }
        public string Token { get; set; }
    }
}
