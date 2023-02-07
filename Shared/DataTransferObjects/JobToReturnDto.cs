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
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string LogoUrl { get; set; }
        public int NumberOfApplicants { get; set; }
        public int NumbersToBeHired { get; set; }
        public DateTime ClosingDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class RawJobToReturnDto
    {
        public string Title { get; set; }
        public string Descriptions { get; set; }
        public double? SalaryLowerRange { get; set; }
        public double? SalaryUpperRange { get; set; }
        public DateTime? ClosingDate { get; set; }
        public Guid CompanyId { get; set; }
        public Guid IndustryId { get; set; }
        public string City { get; set; }
        public Guid StateId { get; set; }
        public Guid CountryId { get; set; }
        public Guid TypeId { get; set; }
        public int NumbersToBeHired { get; set; }
    }
}
