namespace Shared.DataTransferObjects
{
    public sealed record EmailToReturnDto : EmailDto
    {
        public Guid Id { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; init; }
    }
}
