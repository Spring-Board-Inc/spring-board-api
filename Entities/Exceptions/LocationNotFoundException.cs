namespace Entities.Exceptions
{
    public sealed class LocationNotFoundException : NotFoundException
    {
        public LocationNotFoundException(Guid companyId)
            : base($"The location with id: {companyId} doesn't exist in the database.")
        { }
    }
}
