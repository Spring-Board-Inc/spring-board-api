using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class User : IdentityUser
    {
        [Required, Column(TypeName = "nvarchar(80)")]
        public string FirstName { get; set; } = string.Empty;
        [Required, Column(TypeName = "nvarchar(80)")]
        public string LastName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = false;
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public UserInformation? UserInformation { get; set; }
        public string PhotoUrl { get; set; } = string.Empty;
        public string PublicId { get; set; } = string.Empty;
        public ICollection<Token>? Tokens { get; set; }
    }
}