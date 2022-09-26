namespace Entities.Response
{
    public sealed class LocationBadRequestResponse : ApiBadRequestResponse
    {
        public LocationBadRequestResponse() : base("Invalid location request! Please try again.")
        { }
    }
}
