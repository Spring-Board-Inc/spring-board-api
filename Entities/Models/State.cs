using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class State : BaseEntity
    {
        [Required]
        public string AdminArea { get; set; }
        public Guid CountryId { get; set; }
        public Country? Country { get; set; }
    }
}
