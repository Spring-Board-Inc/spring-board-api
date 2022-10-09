using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public class ChangeForgottenPasswordDto
    {
        [Required(ErrorMessage = $"{nameof(Token)} is required")]
        public string Token { get; set; }
        [Required(ErrorMessage = $"{nameof(UserId)} is required")]
        public string UserId { get; set; }
        [Required(ErrorMessage = $"{nameof(NewPassword)} is required")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = $"{nameof(ConfirmNewPassword)} is required"), Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; }
        public bool IsPasswordMatched => NewPassword.Equals(ConfirmNewPassword);
        public bool IsValidEntries => !string.IsNullOrWhiteSpace(Token) && !string.IsNullOrWhiteSpace(UserId);
        public bool IsValidParams => IsValidEntries && IsPasswordMatched;
    }
}
