namespace Shared.RequestFeatures
{
    public class JobSearchParams : RequestParameters
    {
        public Guid IndustryId { get; set; } = Guid.Empty;
        public Guid JobTypeId { get; set; } = Guid.Empty;
    }
}
