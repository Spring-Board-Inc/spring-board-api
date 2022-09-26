﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entities.Models
{
    public class UserInformation : BaseEntity
    {
        public string UserId { get; set; } = string.Empty;
        [JsonIgnore]
        public User? User { get; set; }
        [Required, Column(TypeName = "nvarchar(120)")]
        public string Street { get; set; } = string.Empty;
        [Required, Column(TypeName = "nvarchar(80)")]
        public string Town { get; set; } = string.Empty;
        [Required, Column(TypeName = "nvarchar(10)")]
        public string PostalCode { get; set; } = string.Empty;
        [Required, Column(TypeName = "nvarchar(80)")]
        public string State { get; set; } = string.Empty;
        [Required, Column(TypeName = "nvarchar(80)")]
        public string Country { get; set; } = string.Empty;
        public ICollection<Education>? Educations { get; set; }
        public ICollection<WorkExperience>? WorkExperiences { get; set; }
        public ICollection<UserSkill>? UserSkills { get; set; }
        public ICollection<Certification>? Certifications { get; set; }
    }
}