using Mongo.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Entities.Models
{
    public class Contact : IBaseEntity
    {
        [BsonId]
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeprecated { get; set; }
        public Address? Address { get; set; }
        public ICollection<Phone> Phones { get; set; }
        public ICollection<Email> Email { get; set; }
    }
}
