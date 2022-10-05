using System.ComponentModel.DataAnnotations;

namespace Shared.RequestFeatures
{
    public class EmailRequestParameters
    {
        [Required]
        public string To { get; set; }
        [Required] 
        public string Message { get; set; }
        [Required]
        public string Subject { get; set; }
        public bool IsValidParams => !string.IsNullOrWhiteSpace(To) && !string.IsNullOrWhiteSpace(Message) && !string.IsNullOrWhiteSpace(Subject);
    }
}
