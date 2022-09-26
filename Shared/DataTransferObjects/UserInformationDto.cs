using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public class UserInformationDto
    {
        [Required]
        public string Street { get; set; }
        [Required]
        public string Town { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Country { get; set; }
    }
}