using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class UserJob
    {
        [Key]
        [Column(Order = 1)]
        public Guid JobId { get; set; }
        [Key]
        [Column(Order = 2)]
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsDeprecated { get; set; } = false;
    }
}
