using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Entities.Models
{
    public class UserInformation : BaseEntity
    {
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid UserId { get; set; }
        public ICollection<Education>? Educations { get; set; }
        public ICollection<WorkExperience>? WorkExperiences { get; set; }
        public ICollection<UserSkill>? UserSkills { get; set; }
        public ICollection<Certification>? Certifications { get; set; }
    }
}