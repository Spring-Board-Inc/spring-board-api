namespace Shared.RequestFeatures
{
    public class JobSearchParams : RequestParameters
    {
        public Guid LocationId { get; set; } = Guid.Empty;
        public Guid CompanyId { get; set; } = Guid.Empty;
        public Guid IndustryId { get; set; } = Guid.Empty;
        public Guid JobTypeId { get; set; } = Guid.Empty;
    }
}
