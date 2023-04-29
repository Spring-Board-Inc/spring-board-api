namespace Shared.DataTransferObjects
{
    public sealed record PhoneToReturnDto : PhoneDto
    {
        public Guid Id { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; init; }
    }
}
