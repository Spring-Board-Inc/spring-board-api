using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public class JobTypeRequestObject
    {
        [Required]
        public string? JobType { get; set; }
        public bool IsValidParams => !string.IsNullOrWhiteSpace(JobType);
    }
}
