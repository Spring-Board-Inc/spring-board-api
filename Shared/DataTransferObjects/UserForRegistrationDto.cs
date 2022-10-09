using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public record UserForRegistrationDto
    {
        [Required(ErrorMessage = "First name is required")]
        public string? FirstName { get; init; }
        [Required(ErrorMessage = "Last name is required")]
        public string? LastName { get; init; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; init; }
        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Email address is required"), EmailAddress]
        public string? Email { get; init; }
        [Phone]
        public string? PhoneNumber { get; init; }
        [Required]
        public int RoleIndex { get; set; } = 0;
        [Required]
        public string Gender { get; set; }
        public bool IsPasswordMatched => Password.Equals(ConfirmPassword);
        public bool IsValidEntries => !string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName) && !string.IsNullOrWhiteSpace(Gender) && !string.IsNullOrWhiteSpace(Email) && (RoleIndex >= 0 && RoleIndex <= 3);
        public bool IsValidParams => IsPasswordMatched && IsValidEntries;
    }
}
