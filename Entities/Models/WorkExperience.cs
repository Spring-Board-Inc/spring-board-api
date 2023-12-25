using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class WorkExperience : BaseEntity
    {
        [Required]
        public string Company { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string Designation { get; set; }
        [Required]
        public string? Descriptions { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid UserInformationId { get; set; }
    }
}
