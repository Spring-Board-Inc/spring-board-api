using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public class ChangeForgottenPasswordDto
    {
        [Required]
        public string Token { get; set; } = string.Empty;
        [Required]
        public string UserId { get; set; } = string.Empty;
        [Required]
        public string NewPassword { get; set; } = string.Empty;
        [Required, Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; }
    }
}
