using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class UserSkill
    {
        [Key]
        [Column(Order = 1)]
        public Guid UserInformationId { get; set; }
        [Key]
        [Column(Order = 2)]
        public Guid SkillId { get; set; }
        [Required]
        public string Skill { get; set; }
        [Required]
        public string Level { get; set; }
    }
}
