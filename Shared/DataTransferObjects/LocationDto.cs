using Entities.Models;

namespace Shared.DataTransferObjects
{
    public record LocationDto(
        Guid Id, DateTime CreatedAt, 
        DateTime UpdatedAt, string Town, 
        string State
        );

    public record LocationForCreationDto(string Town, string State);

    public record LocationForUpdateDto(string Town, string State);
}
