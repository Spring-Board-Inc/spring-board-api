using Mongo.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class WorkExperience : IBaseEntity
    {
        [BsonId]
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeprecated { get; set; }
        [Required]
        public string Company { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string Designation { get; set; }
        [Required]
        public string? Descriptions { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid UserInformationId { get; set; }
    }
}
