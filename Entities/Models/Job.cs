using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Job : BaseEntity
    {
        [Required, Column(TypeName = "nvarchar(80)")]
        public string Title { get; set; }
        [Required]
        public string Descriptions { get; set; }
        public double? SalaryLowerRange { get; set; }
        public double? SalaryUpperRange { get; set; }
        public DateTime? ClosingDate { get; set; }
        [ForeignKey(nameof(Company))]
        public Guid CompanyId { get; set; }
        public Company? Company { get; set; }
        [ForeignKey(nameof(Industry))]
        public Guid IndustryId { get; set; }
        public Industry? Industry { get; set; }
        [ForeignKey(nameof(Location))]
        public Guid LocationId { get; set; }
        public Location? Location { get; set; }
        [ForeignKey(nameof(Type))]
        public Guid TypeId { get; set; }
        public JobType? Type { get; set; }
        public int NumberOfApplicants { get; set; } = 0;
    }
}