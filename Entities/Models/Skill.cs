﻿using Mongo.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    [CollectionName("Skills")]
    public class Skill : IBaseEntity
    {
        [BsonId]
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeprecated { get; set; }
        [Required]
        public string Description { get; set; }

    }
}
