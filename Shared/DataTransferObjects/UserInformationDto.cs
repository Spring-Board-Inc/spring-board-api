using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public class UserInformationDto
    {
        [Required(ErrorMessage = $"{nameof(Street)} is required")]
        public string Street { get; set; }
        [Required(ErrorMessage = $"{nameof(Town)} is required")]
        public string Town { get; set; }
        [Required(ErrorMessage = $"{nameof(PostalCode)} is required")]
        public string PostalCode { get; set; }
        [Required(ErrorMessage = $"{nameof(State)} is required")]
        public string State { get; set; }
        [Required(ErrorMessage = $"{nameof(Country)} is required")]
        public string Country { get; set; }
        public bool IsValidParams => !string.IsNullOrWhiteSpace(Street) && !string.IsNullOrWhiteSpace(Town) && !string.IsNullOrWhiteSpace(PostalCode) && !string.IsNullOrWhiteSpace(State) && !string.IsNullOrWhiteSpace(Country);
    }
}