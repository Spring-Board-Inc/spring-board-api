using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Contact : BaseEntity
    {
        public Address? Address { get; set; }
        public ICollection<Phone> Phones { get; set; }
        public ICollection<Email> Email { get; set; }
    }
}
