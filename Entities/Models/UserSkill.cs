using Mongo.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class UserSkill : IBaseEntity
    {
        [BsonId]
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeprecated { get; set; }
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid UserInformationId { get; set; }
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid SkillId { get; set; }
        [Required]
        public string Skill { get; set; }
        [Required]
        public string Level { get; set; }
    }
}
