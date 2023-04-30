namespace Shared.DataTransferObjects
{
    public sealed record ContactForCreationDto
    {
        public AddressForCreationDto Address { get; init; }
        public ICollection<PhoneForCreationDto> Phones { get; init; }
        public ICollection<EmailForCreationDto> Email { get; init; }
    }
}
