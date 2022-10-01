namespace Shared.DataTransferObjects
{
    public class WorkExperienceMinInfo
    {
        public Guid Id { get; set; }
        public string Company { get; set; }
        public string Location { get; set; }
        public string Designation { get; set; }
        public string? Descriptions { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
