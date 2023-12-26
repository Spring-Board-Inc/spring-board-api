using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Industry : BaseEntity
    {
        [Required, Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }
    }
}
