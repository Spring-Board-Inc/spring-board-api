using MongoDbGenericRepository.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    [CollectionName("Skills")]
    public class Skill : BaseEntity
    {
        [Required]
        public string Description { get; set; }

    }
}
