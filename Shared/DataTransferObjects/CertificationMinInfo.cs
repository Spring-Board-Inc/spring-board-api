namespace Shared.DataTransferObjects
{
    public class CertificationMinInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string IssuingBody { get; set; }
        public DateTime IssuingDate { get; set; }
    }
}
