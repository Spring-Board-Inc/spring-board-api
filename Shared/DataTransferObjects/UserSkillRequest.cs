using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public class UserSkillRequest
    {
        [Required]
        public string Level { get; set; }
        public bool IsValidParams => !string.IsNullOrWhiteSpace(Level);
    }
}