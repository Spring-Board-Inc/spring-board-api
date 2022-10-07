using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public class IndustryRequestObject
    {
        [Required(ErrorMessage = $"{nameof(Industry)} is required")]
        public string Industry { get; set; }
        public bool IsValidParams => !string.IsNullOrWhiteSpace(Industry);
    }
}
