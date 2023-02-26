using System.ComponentModel.DataAnnotations;

namespace Shared
{
    public record CareerSummaryDto
    {
        [Required]
        public string CareerSummary { get; set; }
        public bool IsValid => !string.IsNullOrWhiteSpace(CareerSummary);
    }

    public record CareerSummaryUpdateDto
    {
        [Required]
        public string CareerSummary { get; set; }
        public bool IsValid => !string.IsNullOrWhiteSpace(CareerSummary);
    }

    public record CareerSummaryReturnDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string CareerSummary { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
