using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public class EducationForUpdateDto
    {
        [Required(ErrorMessage = $"{nameof(School)} is required")]
        public string School { get; set; }
        [Required(ErrorMessage = $"{nameof(City)} is required")]
        public string City { get; set; }
        [Required(ErrorMessage = $"{nameof(Country)} is required")]
        public string Country { get; set; }
        [Required(ErrorMessage = $"{nameof(Course)} is required")]
        public string Course { get; set; }
        public int Level { get; set; }
        [Required(ErrorMessage = $"{nameof(StartDate)} is required")]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; } = null;
        public bool IsValidDateRange => (EndDate == null && StartDate <= DateTime.Now) || EndDate > StartDate;
        public bool IsValidEntries => !string.IsNullOrWhiteSpace(School) && !string.IsNullOrWhiteSpace(City) && !string.IsNullOrWhiteSpace(Country) && !string.IsNullOrWhiteSpace(Course) && (Level >= 0 && Level <= 5);
        public bool IsValidParams => IsValidDateRange && IsValidEntries;
    }
}
