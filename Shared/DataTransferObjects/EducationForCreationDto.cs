using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public class EducationForCreationDto
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
        public DateTime? EndDate { get; set; } = null;
        public bool IsValidDateRange => (EndDate == null && StartDate <= DateTime.Now) || EndDate > StartDate;
        public bool IsValidEntries => !string.IsNullOrWhiteSpace(School) && !string.IsNullOrWhiteSpace(City) && !string.IsNullOrWhiteSpace(Country) && !string.IsNullOrWhiteSpace(Course) && (Level >= 0 && Level <= 4);
        public bool IsValidParams => IsValidDateRange && IsValidEntries;
    }
}
