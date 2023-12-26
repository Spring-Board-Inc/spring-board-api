using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    [CollectionName("Users")]
    public class AppUser : MongoIdentityUser<Guid>
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Gender { get; set; }
        public bool IsActive { get; set; } = false;
        public bool IsDeprecated { get; set; } = false;
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
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
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid UserInformationId { get; set; }
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid CareerSummaryId { get; set; }
        public string PhotoUrl { get; set; } = string.Empty;
        public string PublicId { get; set; } = string.Empty;
    }
}