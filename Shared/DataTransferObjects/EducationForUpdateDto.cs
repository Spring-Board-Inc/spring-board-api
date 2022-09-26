using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public class EducationForUpdateDto
    {
        [Required]
        public string School { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Course { get; set; }
        public int Level { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsValidDateRange => EndDate != null && EndDate > StartDate;
        public bool IsValidStartDate => StartDate < DateTime.Now;
    }
}
