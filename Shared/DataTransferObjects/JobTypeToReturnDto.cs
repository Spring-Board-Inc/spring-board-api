namespace Shared.DataTransferObjects
{
    public class JobTypeToReturnDto
    {
        public Guid Id { get; set; }
        public string JobType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
