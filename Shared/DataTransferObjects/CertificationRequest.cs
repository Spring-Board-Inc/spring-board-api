using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public class CertificationRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string IssuingBody { get; set; } = string.Empty;
        [Required]
        public DateTime IssuingDate { get; set; }
    }
}
