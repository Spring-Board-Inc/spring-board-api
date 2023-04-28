using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Email : BaseEntity
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        [ForeignKey(nameof(Contact))]
        public Guid ContactId { get; set; }
        public Contact? Contact { get; set; }
    }
}
