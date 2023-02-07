using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Country : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public ICollection<State>? States { get; set; }
    }
}
