using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Education : BaseEntity
    {
        [Required]
        public string School { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Major { get; set; }
        public string LevelOfEducation { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid UserInformationId { get; set; }
    }
}
