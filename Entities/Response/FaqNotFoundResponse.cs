namespace Entities.Response
{
    public sealed class FaqNotFoundResponse : ApiNotFoundResponse
    {
        public FaqNotFoundResponse(Guid id)
            : base($"No FAQ record found with Id: {id}")
        {}
    }
}
