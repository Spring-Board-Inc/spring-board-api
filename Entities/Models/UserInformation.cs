using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Entities.Models
{
    public class UserInformation : IBaseEntity
    {
        [BsonId]
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeprecated { get; set; }
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid UserId { get; set; }
        public ICollection<Education>? Educations { get; set; }
        public ICollection<WorkExperience>? WorkExperiences { get; set; }
        public ICollection<UserSkill>? UserSkills { get; set; }
        public ICollection<Certification>? Certifications { get; set; }
    }
}