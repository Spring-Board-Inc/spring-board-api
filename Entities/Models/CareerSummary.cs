using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class CareerSummary : BaseEntity
    {
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid UserId { get; set; }
        [Required]
        public string Summary { get; set; }
    }
}
