using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public class WorkExperienceRequest
    {
        [Required(ErrorMessage = $"{nameof(Company)} is required")]
        public string Company { get; set; }
        [Required(ErrorMessage = $"{nameof(Location)} is required")]
        public string Location { get; set; }
        [Required(ErrorMessage = $"{nameof(Designation)} is required")]
        public string Designation { get; set; }
        [Required(ErrorMessage = $"{nameof(Descriptions)} is required")]
        public string? Descriptions { get; set; }
        [Required(ErrorMessage = $"{nameof(StartDate)} is required")]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; } = null;
        public bool IsValidDateRange => (EndDate == null && StartDate <= DateTime.Now) || EndDate > StartDate;
        public bool IsValidEntries => !string.IsNullOrWhiteSpace(Company) && !string.IsNullOrWhiteSpace(Location) && !string.IsNullOrWhiteSpace(Designation) && !string.IsNullOrWhiteSpace(Descriptions);
        public bool IsValidParams => IsValidDateRange && IsValidEntries;
    }
}
