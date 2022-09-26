using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public class PhotoToUploadDto
    {
        [Required]
        public IFormFile Photo { get; set; }
    }
}
