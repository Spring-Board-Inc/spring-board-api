using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public abstract record AddressForManipulationDto
    {
        [Required]
        public string Line1 { get; init; }
        public string Line2 { get; init; }
        [Required]
        public string City { get; init; }
        [Required]
        public string PostalCode { get; init; }
        [Required]
        public string Country { get; init; }
    }
}
