using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Token
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [ForeignKey(nameof(Users))]
        public string UserId { get; set; }
        public AppUser? Users { get; set; }
        [Required]
        public string Value { get; set; }
        [Required]
        public string Type { get; set; }
        public DateTime ExpiresAt { get; set; } = DateTime.Now.AddDays(7);
    }
}