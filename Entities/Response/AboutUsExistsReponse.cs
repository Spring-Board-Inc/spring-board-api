namespace Entities.Response
{
    public sealed class AboutUsExistsReponse : ApiBadRequestResponse
    {
        public AboutUsExistsReponse()
            : base("You can only add a single record. Delete the existing to add another.")
        {}
    }
}
