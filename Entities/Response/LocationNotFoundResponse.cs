namespace Entities.Response
{
    public sealed class LocationNotFoundResponse : ApiNotFoundResponse
    {
        public LocationNotFoundResponse(Guid id) : base($"Location with id: {id} is not found in db.")
        { }
    }
}
