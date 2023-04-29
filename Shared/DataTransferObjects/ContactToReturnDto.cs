namespace Shared.DataTransferObjects
{
    public sealed record ContactToReturnDto
    {
        public AddressToReturnDto Address { get; init; }
        public ICollection<PhoneToReturnDto> Phone { get; init; }
        public ICollection<EmailToReturnDto> Email { get; init; }
    }
}
