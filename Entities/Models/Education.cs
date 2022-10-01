using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Enums;

namespace Entities.Models
{
    public class Education : BaseEntity
    {
        [Required, Column(TypeName = "nvarchar(100)")]
        public string School { get; set; }
        [Required, Column(TypeName = "nvarchar(80)")]
        public string City { get; set; }
        [Required, Column(TypeName = "nvarchar(100)")]
        public string Country { get; set; }
        [Required, Column(TypeName = "nvarchar(100)")]
        public string Major { get; set; }
        public string LevelOfEducation { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [ForeignKey(nameof(UserInformation))]
        public Guid UserInformationId { get; set; }
        public UserInformation? UserInformation { get; set; }
    }
}
