using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public class SkillRequest
    {
        [Required(ErrorMessage = $"{nameof(Description)} is required")]
        public string Description { get; set; }
        public bool IsValidParams => !string.IsNullOrWhiteSpace(Description);
    }
}