namespace Shared.DataTransferObjects
{
    public record EducationToReturnDto
    {
        public Guid Id { get; set; }
        public string School { get; set; }
        public string Course { get; set; }
        public string LevelOfEducation { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
