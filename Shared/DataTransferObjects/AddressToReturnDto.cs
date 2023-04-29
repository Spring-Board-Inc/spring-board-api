namespace Shared.DataTransferObjects
{
    public sealed record AddressToReturnDto : AddressForManipulationDto
    {
        public Guid Id { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; init; }
    }
}
