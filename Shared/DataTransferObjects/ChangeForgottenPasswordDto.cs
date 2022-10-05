using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public class ChangeForgottenPasswordDto
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required, Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; }
        public bool IsPasswordMatched => NewPassword.Equals(ConfirmNewPassword);
        public bool IsValidEntries => !string.IsNullOrWhiteSpace(Token) && !string.IsNullOrWhiteSpace(UserId);
        public bool IsValidParams => IsValidEntries && IsPasswordMatched;
    }
}
