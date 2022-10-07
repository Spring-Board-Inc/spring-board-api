using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public class JobRequestObject
    {
        [Required(ErrorMessage = $"{nameof(Title)} is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = $"{nameof(Descriptions)} is required")]
        public string? Descriptions { get; set; }
        public double? SalaryLowerRange { get; set; } = 0;
        public double? SalaryUpperRange { get; set; } = 0;
        public DateTime? ClosingDate { get; set; } = null;
        [Required(ErrorMessage = $"{nameof(CompanyId)} is required")]
        public Guid CompanyId { get; set; }
        [Required(ErrorMessage = $"{nameof(IndustryId)} is required")]
        public Guid IndustryId { get; set; }
        [Required(ErrorMessage = $"{nameof(LocationId)} is required")]
        public Guid LocationId { get; set; }
        [Required(ErrorMessage = $"{nameof(TypeId)} is required")]
        public Guid TypeId { get; set; }
        public bool IsValidSalaryRange => SalaryUpperRange >= SalaryLowerRange;
        public bool IsValidClosingDate => ClosingDate == null || ClosingDate > DateTime.Now;
        public bool IsValidIds => !CompanyId.Equals(Guid.Empty) && !IndustryId.Equals(Guid.Empty) && !LocationId.Equals(Guid.Empty) && !TypeId.Equals(Guid.Empty);
        public bool IsValidEntries => !string.IsNullOrWhiteSpace(Title) && !string.IsNullOrWhiteSpace(Descriptions);
        public bool IsValidParams => IsValidClosingDate && IsValidIds && IsValidEntries && IsValidSalaryRange;
    }
}
