using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public class ChangePasswordDto
    {
        [Required]
        public string CurrentPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required, Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; }
        public bool IsPasswordMatched => NewPassword.Equals(ConfirmNewPassword);
    }
}
