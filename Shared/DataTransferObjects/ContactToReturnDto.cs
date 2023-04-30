namespace Shared.DataTransferObjects
{
    public sealed record ContactToReturnDto
    {
        public Guid Id { get; init; }
        public AddressToReturnDto Address { get; init; }
        public ICollection<PhoneToReturnDto> Phones { get; init; }
        public ICollection<EmailToReturnDto> Email { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; init; }
    }
}
