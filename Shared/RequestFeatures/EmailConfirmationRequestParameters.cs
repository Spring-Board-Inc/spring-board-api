using System.ComponentModel.DataAnnotations;

namespace Shared.RequestFeatures
{
    public class EmailConfirmationRequestParameters
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string UserId { get; set; }
        public bool IsValidParams => !string.IsNullOrWhiteSpace(UserId) && !string.IsNullOrWhiteSpace(Token);
    }
}
