using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Shared.DataTransferObjects;

[Route("api/token")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly IServiceManager _service;
    public TokenController(IServiceManager service) => _service = service;

    /// <summary>
    /// End-point to renew refresh tokens
    /// </summary>
    /// <param name="tokenDto"></param>
    /// <returns>Access and refresh tokens.</returns>
    ///<response code="200">Ok. If everything goes well.</response>
    ///<response code="204">No content. Everything is ok.</response>
    ///<response code="404">Not found. If resource(s) not found.</response>
    ///<response code="400">Bad request. If the request is not valid.</response>
    ///<response code="401">Unauthorized. Invalid authentication credentials for the requested resource.</response>
    ///<response code="403">Forbidden. Server refuses to authorize the request.</response>
    ///<response code="500">Server error. If the server did not understand the request.</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost("refresh")]
    [Authorize]
    public async Task<IActionResult> Refresh([FromBody] TokenDto tokenDto)
    {
        var tokenDtoToReturn = await _service.Authentication.RefreshToken(tokenDto);
        return Ok(tokenDtoToReturn);
    }
}