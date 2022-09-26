namespace Shared.DataTransferObjects
{
    public class GetEmailTemplateDto
    {
        public string Url { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public int TemplateType { get; set; }
    }
}
