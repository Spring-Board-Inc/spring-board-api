namespace Entities.Response
{
    public sealed class UserNotFoundResponse : ApiNotFoundResponse
    {
        public UserNotFoundResponse(string message) : base(message)
        { }
    }
}
