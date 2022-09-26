namespace Entities.Response
{
    public sealed class UnauthorizeResponse : ApiUnathorizedResponse
    {
        public UnauthorizeResponse(string message) : base(message)
        { }
    }
}