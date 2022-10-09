using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public class PhotoToUploadDto
    {
        [Required(ErrorMessage = $"{nameof(Photo)} is required")]
        public IFormFile Photo { get; set; }
        public bool IsValidParams => Photo?.Length > 0;
    }
}
