namespace Shared.DataTransferObjects
{
    public record AboutUsToReturnDto : AboutUsForManipulationDto
    {
        public Guid Id { get; init; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
