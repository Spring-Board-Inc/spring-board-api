using Entities.Models;

namespace Shared.DataTransferObjects
{
    public record LocationDto(
        Guid Id, DateTime CreatedAt, 
        DateTime UpdatedAt, string Town, 
        string State
        );

    public record LocationForCreationDto
    {
        public string Town { get; set; }
        public string State { get; set; }
        public bool IsValidParams => !string.IsNullOrWhiteSpace(Town) && !string.IsNullOrWhiteSpace(State);
    }

    public record LocationForUpdateDto
    {
        public string Town { get; set; }
        public string State { get; set; }
        public bool IsValidParams => !string.IsNullOrWhiteSpace(Town) && !string.IsNullOrWhiteSpace(State);
    }
}
