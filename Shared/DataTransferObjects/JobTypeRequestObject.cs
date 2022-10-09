using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public class JobTypeRequestObject
    {
        [Required(ErrorMessage = $"{nameof(JobType)} is required")]
        public string JobType { get; set; }
        public bool IsValidParams => !string.IsNullOrWhiteSpace(JobType);
    }
}
