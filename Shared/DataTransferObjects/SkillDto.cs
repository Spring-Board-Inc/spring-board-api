namespace Shared.DataTransferObjects
{
    public record SkillDto
    {
        public Guid Id { get; init; }
        public string Description { get; init; }
    }
}
