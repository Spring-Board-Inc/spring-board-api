using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Job : IBaseEntity
    {
        [BsonId]
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeprecated { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Descriptions { get; set; }
        public double? SalaryLowerRange { get; set; }
        public double? SalaryUpperRange { get; set; }
        public DateTime? ClosingDate { get; set; }
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid CompanyId { get; set; }
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid IndustryId { get; set; }
        public string City { get; set; }
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid StateId { get; set; }
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid CountryId { get; set; }
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid TypeId { get; set; }
        public int NumbersToBeHired { get; set; } = 1;
        public int NumberOfApplicants { get; set; } = 0;
    }
}