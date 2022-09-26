using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Company : BaseEntity
    {
        [Required, Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; } = string.Empty;
        [Required, EmailAddress, Column(TypeName = "nvarchar(100)")]
        public string Email { get; set; } = string.Empty;
        public string LogoUrl { get; set; } = string.Empty;
        public string PublicId { get; set; } = string.Empty;
        public ICollection<Job>? Jobs { get; set; }
    }
}
