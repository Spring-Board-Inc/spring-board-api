using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public class UserForAuthenticationDto
    {
        [Required(ErrorMessage = $"{nameof(Email)} is required")]
        public string? Email { get; init; }
        [Required(ErrorMessage = $"{nameof(Password)} is required")]
        public string? Password { get; init; }
        public bool RememberMe { get; init; }
        public bool IsValidParams => !string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(Email);
    }
}
