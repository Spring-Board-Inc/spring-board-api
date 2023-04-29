namespace Entities.Response
{
    public sealed class ContactExistsResponse : ApiBadRequestResponse
    {
        public ContactExistsResponse()
            : base("Contact record exists. Please deprecate or delete the existing to add another.")
        {}
    }
}
