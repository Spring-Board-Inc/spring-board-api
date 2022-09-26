namespace Entities.Response
{
    public sealed class NotFoundResponse : ApiNotFoundResponse
    {
        public NotFoundResponse(string message) : base(message)
        { }
    }
}
