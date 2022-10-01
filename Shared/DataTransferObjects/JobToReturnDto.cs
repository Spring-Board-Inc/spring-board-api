namespace Shared.DataTransferObjects
{
    public class JobToReturnDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Descriptions { get; set; }
        public double SalaryLowerRange { get; set; }
        public double SalaryUpperRange { get; set; }
        public string Industry { get; set; }
        public string JobType { get; set; }
        public string Town { get; set; }
        public string State { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string LogoUrl { get; set; }
        public DateTime ClosingDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
