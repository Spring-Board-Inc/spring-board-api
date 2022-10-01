namespace Shared.DataTransferObjects
{
    public class EducationMinInfo
    {
        public Guid Id { get; set; }
        public string School { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Major { get; set; }
        public string LevelOfEducation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
