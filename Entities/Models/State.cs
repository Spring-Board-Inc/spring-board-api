using Mongo.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class State : IBaseEntity
    {
        [BsonId]
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeprecated { get; set; }
        [Required]
        public string AdminArea { get; set; }
        public Guid CountryId { get; set; }
        public Country? Country { get; set; }
    }
}
