using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Token
    {
        [BsonId]
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid Id { get; set; }
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid UserId { get; set; }
        [Required]
        public string Value { get; set; }
        [Required]
        public string Type { get; set; }
        public DateTime ExpiresAt { get; set; } = DateTime.Now.AddDays(7);
    }
}