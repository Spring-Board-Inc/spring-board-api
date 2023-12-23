using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Certification : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string IssuingBody { get; set; }
        [Required]
        public DateTime IssuingDate { get; set; }
        public Guid UserInformationId { get; set; }
    }
}