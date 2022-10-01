using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class JobType : BaseEntity
    {
        [Required, Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }
        public ICollection<Job>? Jobs { get; set; }
    }
}
