using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Phone : BaseEntity
    {
        [Required, Phone]
        public string PhoneNumber { get; set; }
        public string Ext { get; set; }
        [ForeignKey(nameof(Contact))]
        public Guid ContactId { get; set; }
        public Contact? Contact { get; set; }
    }
}
