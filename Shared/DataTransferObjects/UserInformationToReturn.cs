﻿using Entities.Models;

namespace Shared.DataTransferObjects
{
    public class UserInformationToReturn
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string Street { get; set; }
        public string Town { get; set; }
        public string PostalCode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<EducationMinInfo> Educations { get; set; }
        public ICollection<WorkExperienceMinInfo> WorkExperiences { get; set; }
        public ICollection<CertificationMinInfo> Certifications { get; set; }
        public ICollection<UserSkillMinInfo> UserSkills { get; set; }
    }
}
