using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class UserSkill
    {
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
