namespace Shared.DataTransferObjects
{
    public class IndustryToReturnDto
    {
        public Guid Id { get; set; }
        public string Industry { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
