namespace Shared.DataTransferObjects
{
    public sealed record ContactForUpdateDto
    {
        public AddressForUpdateDto Address { get; init; }
        public ICollection<PhoneForUpdateDto> Phones { get; init; }
        public ICollection<EmailForUpdateDto> Email { get; init; }
    }
}
