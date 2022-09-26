using System.ComponentModel.DataAnnotations;

namespace Shared.RequestFeatures
{
    public class EmailConfirmationRequestParameters
    {
        [Required]
        public string Token { get; set; } = string.Empty;
        [Required]
        public string UserId { get; set; } = string.Empty;
    }
}
