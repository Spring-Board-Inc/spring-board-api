using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public class RoleForCreateDto
    {
        [Required]
        public string RoleName { get; init; }
    }
}
