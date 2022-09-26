namespace Entities.Response
{
    public sealed class BadRequestResponse : ApiBadRequestResponse
    {
        public BadRequestResponse(string message) : base(message)
        {

        }
    }
}
