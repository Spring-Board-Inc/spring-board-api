using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public class ResetPasswordDto
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
