using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public class JobRequestObject
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string? Descriptions { get; set; }
        public double? SalaryLowerRange { get; set; } = 0;
        public double? SalaryUpperRange { get; set; } = 0;
        public DateTime? ClosingDate { get; set; } = null;
        [Required]
        public Guid CompanyId { get; set; }
        [Required]
        public Guid IndustryId { get; set; }
        [Required]
        public Guid LocationId { get; set; }
        [Required]
        public Guid TypeId { get; set; }
        public bool IsValidSalaryRange => SalaryUpperRange >= SalaryLowerRange;
        public bool IsValidClosingDate => ClosingDate == null || ClosingDate > DateTime.Now;
        public bool IsValidIds => !CompanyId.Equals(Guid.Empty) && !IndustryId.Equals(Guid.Empty) && !LocationId.Equals(Guid.Empty) && !TypeId.Equals(Guid.Empty);
        public bool IsValidEntries => !string.IsNullOrWhiteSpace(Title) && !string.IsNullOrWhiteSpace(Descriptions);
        public bool IsValidParams => IsValidClosingDate && IsValidIds && IsValidEntries && IsValidSalaryRange;
    }
}
