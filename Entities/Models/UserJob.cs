using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Entities.Models
{
    public class UserJob
    {
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid JobId { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeprecated { get; set; } = false;
    }
}