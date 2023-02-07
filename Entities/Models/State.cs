using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class State : BaseEntity
    {
        [Required]
        public string AdminArea { get; set; }
        [ForeignKey(nameof(Country))]
        public Guid CountryId { get; set; }
        public Country? Country { get; set; }
    }
}
