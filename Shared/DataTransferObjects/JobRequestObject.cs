using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public class JobRequestObject
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string? Descriptions { get; set; }
        public double? SalaryLowerRange { get; set; }
        public double? SalaryUpperRange { get; set; }
        public DateTime? ClosingDate { get; set; }
        [Required]
        public Guid CompanyId { get; set; }
        public Guid IndustryId { get; set; }
        [Required]
        public Guid LocationId { get; set; }
        public Guid TypeId { get; set; }
        public bool IsValidSalaryRange => SalaryUpperRange > SalaryLowerRange;
        public bool IsValidClosingDate => ClosingDate != null && ClosingDate > DateTime.Now;
    }
}
