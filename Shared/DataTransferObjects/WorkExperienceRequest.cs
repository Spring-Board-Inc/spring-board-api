using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public class WorkExperienceRequest
    {
        [Required]
        public string Company { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string Designation { get; set; }
        [Required]
        public string? Descriptions { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsValidDateRange => EndDate != null && EndDate > StartDate;
        public bool IsValidStartDate => StartDate < DateTime.Now;
    }
}
