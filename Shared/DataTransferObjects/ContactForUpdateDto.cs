namespace Shared.DataTransferObjects
{
    public sealed record ContactForUpdateDto
    {
        public AddressForCreationDto Address { get; init; }
        public ICollection<PhoneForUpdateDto> Phone { get; init; }
        public ICollection<EmailForUpdateDto> Email { get; init; }
    }
}
