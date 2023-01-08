using System.Text.Json.Serialization;

namespace Entities.Models
{
    public class UserInformation : BaseEntity
    {
        public string UserId { get; set; }
        [JsonIgnore]
        public AppUser? User { get; set; }
        public ICollection<Education>? Educations { get; set; }
        public ICollection<WorkExperience>? WorkExperiences { get; set; }
        public ICollection<UserSkill>? UserSkills { get; set; }
        public ICollection<Certification>? Certifications { get; set; }
    }
}