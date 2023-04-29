namespace Entities.Response
{
    public sealed class AboutUsNotFoundResponse : ApiNotFoundResponse
    {
        public AboutUsNotFoundResponse(Guid id)
            : base($"No about record with Id: {id} found.")
        {}

        public AboutUsNotFoundResponse()
            : base("No about record found.")
        {}
    }
}
