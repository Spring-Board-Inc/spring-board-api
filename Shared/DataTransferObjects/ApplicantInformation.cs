namespace Shared.DataTransferObjects
{
    public class ApplicantInformation
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PhotoUrl { get; set; }
        public string Gender { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public ICollection<EducationMinInfo> Educations { get; set; }
        public ICollection<WorkExperienceMinInfo> WorkExperiences { get; set; }
        public ICollection<CertificationMinInfo> Certifications { get; set; }
        public ICollection<UserSkillMinInfo> UserSkills { get; set; }
    }
}
