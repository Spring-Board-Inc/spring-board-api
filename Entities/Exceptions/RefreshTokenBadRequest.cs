using Entities.Exceptions;

public sealed class RefreshTokenBadRequest : BadRequestException
{
    public RefreshTokenBadRequest()
    : base("Invalid client request. The token object has some invalid values.")
    { }
}