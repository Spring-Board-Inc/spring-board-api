using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public class UserAddressForUpdateDto
    {
        [Required(ErrorMessage = $"{nameof(Street)} is required")]
        public string Street { get; set; }
        [Required(ErrorMessage = $"{nameof(City)} is required")]
        public string City { get; set; }
        [Required(ErrorMessage = $"{nameof(PostalCode)} is required")]
        public string PostalCode { get; set; }
        [Required(ErrorMessage = $"{nameof(PostalCode)} is required")]
        public string State { get; set; }
        [Required(ErrorMessage = $"{nameof(Country)} is required")]
        public string Country { get; set; }
        public bool IsValidParams => !string.IsNullOrWhiteSpace(Street) && !string.IsNullOrWhiteSpace(City) && 
            !string.IsNullOrWhiteSpace(State) && !string.IsNullOrWhiteSpace(Country) && !string.IsNullOrWhiteSpace(PostalCode);
    }
}
