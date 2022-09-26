using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Token
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [ForeignKey(nameof(Users))]
        public string UserId { get; set; } = string.Empty;
        public User? Users { get; set; }
        [Required]
        public string Value { get; set; } = string.Empty;
        [Required]
        public string Type { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; } = DateTime.Now.AddDays(7);
    }
}