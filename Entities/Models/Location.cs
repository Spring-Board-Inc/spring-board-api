using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Location : BaseEntity
    {
        [Required, Column(TypeName = "nvarchar(80)")]
        public string Town { get; set; }
        [Required, Column(TypeName = "nvarchar(80)")]
        public string State { get; set; }
        public ICollection<Job>? Jobs { get; set; }
    }
}
