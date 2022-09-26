namespace Entities.Response
{
    public abstract class ApiUnathorizedResponse : ApiBaseResponse
    {
        public string Message { get; set; }
        public ApiUnathorizedResponse(string message) : base(false)
        {
            Message = message;
        }
    }
}
