using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public class JobRequestObject
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string? Descriptions { get; set; } = string.Empty;
        public double? SalaryLowerRange { get; set; } = 0;
        public double? SalaryUpperRange { get; set; } = 0;
        public DateTime? ClosingDate { get; set; } = DateTime.MinValue;
        [Required]
        public Guid CompanyId { get; set; }
        [Required]
        public Guid IndustryId { get; set; }
        [Required]
        public Guid LocationId { get; set; }
        [Required]
        public Guid TypeId { get; set; }
        public bool IsValidSalaryRange => SalaryUpperRange >= SalaryLowerRange;
        public bool IsValidClosingDate => ClosingDate > DateTime.Now;
    }
}
