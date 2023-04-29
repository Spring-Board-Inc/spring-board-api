namespace Entities.Response
{
    public sealed class ContactNotFoundResponse : ApiNotFoundResponse
    {
        public ContactNotFoundResponse()
            : base("No contact record found")
        {}

        public ContactNotFoundResponse(Guid id)
            : base($"No contact record found for the Id: {id}")
        {}
    }
}
