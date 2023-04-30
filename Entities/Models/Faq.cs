using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Faq : BaseEntity
    {
        [Required]
        public string Question { get; set; }
        [Required]
        public string Answer { get; set; }
    }
}
