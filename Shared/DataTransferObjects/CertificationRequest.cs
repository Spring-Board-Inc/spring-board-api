using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public class CertificationRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string IssuingBody { get; set; }
        [Required]
        public DateTime IssuingDate { get; set; }
        public bool IsValidParams => !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(IssuingBody) && IssuingDate <= DateTime.Now;
    }
}
