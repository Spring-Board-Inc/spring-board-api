using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public class ResetPasswordDto
    {
        [Required(ErrorMessage = $"{nameof(Email)} is required"), EmailAddress]
        public string Email { get; set; }
        public bool IsValidParams => !string.IsNullOrWhiteSpace(Email);
    }
}
