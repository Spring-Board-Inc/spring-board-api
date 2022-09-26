using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Location : BaseEntity
    {
        [Required, Column(TypeName = "nvarchar(80)")]
        public string Town { get; set; } = string.Empty;
        [Required, Column(TypeName = "nvarchar(80)")]
        public string State { get; set; } = string.Empty;
        public ICollection<Job>? Jobs { get; set; }
    }
}
