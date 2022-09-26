namespace Entities.Response
{
    public sealed class UserUnauthorizeResponse : ApiUnathorizedResponse
    {
        public UserUnauthorizeResponse(string message) : base(message)
        {}
    }
}
