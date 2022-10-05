namespace Shared.DataTransferObjects
{
    public class IndustryRequestObject
    {
        public string Industry { get; set; }
        public bool IsValidParams => !string.IsNullOrWhiteSpace(Industry);
    }
}
