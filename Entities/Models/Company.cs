using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Company : BaseEntity
    {
        [Required, Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }
        [Required, EmailAddress, Column(TypeName = "nvarchar(100)")]
        public string Email { get; set; }
        public string LogoUrl { get; set; }
        public string PublicId { get; set; }
        public string UserId { get; set; }
        public ICollection<Job>? Jobs { get; set; }
    }
}
