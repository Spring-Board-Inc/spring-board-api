namespace Shared.DataTransferObjects
{
    public abstract record ContactForCreationDto
    {
        public AddressForCreationDto Address { get; init; }
        public ICollection<PhoneForCreationDto> Phone { get; init; }
        public ICollection<EmailForCreationDto> Email { get; init; }
    }
}
