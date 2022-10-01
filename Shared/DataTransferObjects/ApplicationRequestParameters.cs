using Microsoft.AspNetCore.Http;

namespace Shared.DataTransferObjects
{
    public class ApplicationRequestParameters
    {
        public string To { get; set; }
        public string Message { get; set; }
        public string Subject { get; set; }
        public IFormFile File { get; set; }
    }
}
