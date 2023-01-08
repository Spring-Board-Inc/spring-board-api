using Entities.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class AppUser : IdentityUser
    {
        [Required, Column(TypeName = "nvarchar(80)")]
        public string FirstName { get; set; }
        [Required, Column(TypeName = "nvarchar(80)")]
        public string LastName { get; set; }
        public string Gender { get; set; }
        public bool IsActive { get; set; } = false;
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Country { get; set; }
        public UserInformation? UserInformation { get; set; }
        public string PhotoUrl { get; set; } = string.Empty;
        public string PublicId { get; set; } = string.Empty;
        public ICollection<Token>? Tokens { get; set; }
    }
}