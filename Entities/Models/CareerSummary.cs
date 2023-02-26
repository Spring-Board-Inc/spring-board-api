using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Entities.Models
{
    public class CareerSummary : BaseEntity
    {
        public string UserId { get; set; }
        [JsonIgnore]
        public AppUser? User { get; set; }
        [Required]
        public string Summary { get; set; }
    }
}
