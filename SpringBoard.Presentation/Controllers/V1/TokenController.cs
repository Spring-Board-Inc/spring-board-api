using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Shared.DataTransferObjects;

[Route("api/token")]
[ApiController]
[Authorize]
public class TokenController : ControllerBase
{
    private readonly IServiceManager _service;
    public TokenController(IServiceManager service) => _service = service;

    /// <summary>
    /// End-point to renew refresh tokens
    /// </summary>
    /// <param name="tokenDto"></param>
    /// <returns>Access and refresh tokens.</returns>
    ///<response code="200">Ok</response>
    ///<response code="400">Bad request</response>
    ///<response code="401">Unauthorized</response>
    ///<response code="403">Forbidden</response>
    ///<response code="500">Server error</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost("refresh")]
    [Authorize]
    public async Task<IActionResult> Refresh([FromBody] TokenDto tokenDto)
    {
        var tokenDtoToReturn = await _service.Authentication.RefreshToken(tokenDto);
        return Ok(tokenDtoToReturn);
    }
}