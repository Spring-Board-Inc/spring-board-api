using System.ComponentModel.DataAnnotations;

namespace Shared.RequestFeatures
{
    public class EmailRequestParameters
    {
        [Required]
        public string To { get; set; } = string.Empty;
        [Required] 
        public string Message { get; set; } = string.Empty;
        [Required]
        public string Subject { get; set; } = string.Empty;
    }
}
