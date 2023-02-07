namespace Shared.DataTransferObjects
{
    public class JobMinimumInfoDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Descriptions { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string JobType { get; set; }
        public string Industry { get; set; }
        public double SalaryLowerRange { get; set; }
        public double SalaryUpperRange { get; set; }
        public DateTime ClosingDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
