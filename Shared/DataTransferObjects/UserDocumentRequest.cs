using Microsoft.AspNetCore.Http;

namespace Shared.DataTransferObjects
{
    public class UserDocumentRequest
    {
        public IFormFile Document { get; set; }
        public string DocType { get; set; }
    }
}
