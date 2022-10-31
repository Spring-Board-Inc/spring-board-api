using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public class ChangePasswordDto
    {
        [Required(ErrorMessage = $"{nameof(CurrentPassword)} is required")]
        public string CurrentPassword { get; set; }
        [Required(ErrorMessage = $"{nameof(NewPassword)} is required")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = $"{nameof(ConfirmNewPassword)} is required"), Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; }
        public string UserId { get; set; }
        public bool IsPasswordMatched => NewPassword.Equals(ConfirmNewPassword);
    }
}
