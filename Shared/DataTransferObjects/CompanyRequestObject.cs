using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public class CompanyRequestObject
    {
        [Required(ErrorMessage = $"{nameof(Name)} is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = $"{nameof(Email)} is required"), EmailAddress]
        public string Email { get; set; }
        public IFormFile? Logo { get; set; }
        public bool IsValidFile => Logo?.Length > 0;
        public bool IsValidParams => !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Name);
    }
}
