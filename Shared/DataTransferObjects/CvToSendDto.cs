using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public class CvToSendDto
    {
        [Required(ErrorMessage = $"{nameof(Cv)} is required")]
        public IFormFile Cv { get; set; }
        public bool IsValidParams => Cv?.Length > 0;
    }
}
