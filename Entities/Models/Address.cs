using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Entities.Models
{
    public class Address : BaseEntity
    {
        [Required]
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string Country { get; set; }
        public Guid ContactId { get; set; }
        [JsonIgnore]
        public Contact? Contact { get; set; }
    }
}
