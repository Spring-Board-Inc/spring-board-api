using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Job : BaseEntity
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Descriptions { get; set; }
        public double? SalaryLowerRange { get; set; }
        public double? SalaryUpperRange { get; set; }
        public DateTime? ClosingDate { get; set; }
        public Company? Company { get; set; }
        public Industry? Industry { get; set; }
        public string City { get; set; }
        public State? State { get; set; }
        public Country? Country { get; set; }
        public JobType? Type { get; set; }
        public int NumbersToBeHired { get; set; } = 1;
        public int NumberOfApplicants { get; set; } = 0;
    }
}