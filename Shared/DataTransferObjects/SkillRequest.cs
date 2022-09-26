using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public class SkillRequest
    {
        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }
    }
}