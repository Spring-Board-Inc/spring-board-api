using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Skill : BaseEntity
    {
        [Required, Column(TypeName = "nvarchar(30)")]
        public string Description { get; set; }
        
    }
}
