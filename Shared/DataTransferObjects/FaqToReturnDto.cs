namespace Shared.DataTransferObjects
{
    public sealed record FaqToReturnDto : FaqBaseDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
