using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class WorkExperience : BaseEntity
    {
        [Required, Column(TypeName = "nvarchar(100)")]
        public string Company { get; set; } = string.Empty;
        [Required, Column(TypeName = "nvarchar(80)")]
        public string Location { get; set; } = string.Empty;
        [Required, Column(TypeName = "nvarchar(50)")]
        public string Designation { get; set; } = string.Empty;
        [Required, Column(TypeName = "nvarchar(450)")]
        public string? Descriptions { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [ForeignKey(nameof(UserInformation))]
        public Guid UserInformationId { get; set; }
        public UserInformation? UserInformation { get; set; }
    }
}
