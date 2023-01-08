using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public record UserForRegistrationDto
    {
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Email address is required"), EmailAddress]
        public string? Email { get; set; }
        [Phone]
        public string? PhoneNumber { get; set; }
        [Required]
        public int RoleIndex { get; set; } = 0;
        [Required]
        public string Gender { get; set; }
        [Required(ErrorMessage = $"{nameof(Street)} is required")]
        public string Street { get; set; }
        [Required(ErrorMessage = $"{nameof(City)} is required")]
        public string City { get; set; }
        [Required(ErrorMessage = $"{nameof(PostalCode)} is required")]
        public string PostalCode { get; set; }
        [Required(ErrorMessage = $"{nameof(State)} is required")]
        public string State { get; set; }
        [Required(ErrorMessage = $"{nameof(Country)} is required")]
        public string Country { get; set; }
        public bool IsPasswordMatched => Password.Equals(ConfirmPassword);
        public bool IsValidEntries => !string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName) && 
            !string.IsNullOrWhiteSpace(Gender) && !string.IsNullOrWhiteSpace(Email) && (RoleIndex >= 0 && RoleIndex <= 3) && 
            !string.IsNullOrWhiteSpace(Street) && !string.IsNullOrWhiteSpace(City) && !string.IsNullOrWhiteSpace(PostalCode) 
            && !string.IsNullOrWhiteSpace(State) && !string.IsNullOrWhiteSpace(Country);
        public bool IsValidParams => IsPasswordMatched && IsValidEntries;
    }

    public record AddressDto
    {
        
    }
}
