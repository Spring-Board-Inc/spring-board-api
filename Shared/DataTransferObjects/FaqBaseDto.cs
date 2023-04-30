using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public abstract record FaqBaseDto
    {
        [Required]
        public string Question { get; init; }
        [Required]
        public string Answer { get; init; }
    }
}
