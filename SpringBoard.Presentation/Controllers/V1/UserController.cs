using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using SpringBoard.Presentation.Controllers.V1;
using SpringBoard.Presentation.Controllers.V1.Extensions;

[ApiVersion("1.0")]
[Route("api/users")]
[ApiController]
public class UserController : ApiControllerBase
{
    private readonly IServiceManager _service;

    public UserController(IServiceManager service) => _service = service;

    ///<summary>
    ///End-point to get a user profile information
    ///</summary>
    ///<param name="userId"></param>
    ///<response code="200">Ok. If everything goes well.</response>
    ///<response code="204">No content. Everything is ok.</response>
    ///<response code="404">Not found. If resource(s) not found.</response>
    ///<response code="400">Bad request. If the request is not valid.</response>
    ///<response code="401">Unauthorized. Invalid authentication credentials for the requested resource.</response>
    ///<response code="403">Forbidden. Server refuses to authorize the request.</response>
    ///<response code="500">Server error. If the server did not understand the request.</response>
    [HttpGet]
    [Route("{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUser(string userId)
    {
        var baseResult = await _service.User.Get(userId);
        if (!baseResult.Success)
            return ProcessError(baseResult);

        var result = baseResult.GetResult<DetailedUserToReturnDto>();
        return Ok(result);
    }

    ///<summary>
    ///End-point to get paginated list of user profile information
    ///</summary>
    ///<param name="searchParameters"></param>
    ///<response code="200">Ok. If everything goes well.</response>
    ///<response code="204">No content. Everything is ok.</response>
    ///<response code="404">Not found. If resource(s) not found.</response>
    ///<response code="400">Bad request. If the request is not valid.</response>
    ///<response code="401">Unauthorized. Invalid authentication credentials for the requested resource.</response>
    ///<response code="403">Forbidden. Server refuses to authorize the request.</response>
    ///<response code="500">Server error. If the server did not understand the request.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUsers([FromQuery] SearchParameters searchParameters)
    {
        var baseResult = await _service.User.Get(searchParameters);
        var result = baseResult.GetResult<PaginatedListDto<DetailedUserToReturnDto>>();
        return Ok(result);
    }

    ///<summary>
    ///End-point to activate the user profile
    ///</summary>
    ///<param name="userId"></param>
    ///<response code="200">Ok. If everything goes well.</response>
    ///<response code="204">No content. Everything is ok.</response>
    ///<response code="404">Not found. If resource(s) not found.</response>
    ///<response code="400">Bad request. If the request is not valid.</response>
    ///<response code="401">Unauthorized. Invalid authentication credentials for the requested resource.</response>
    ///<response code="403">Forbidden. Server refuses to authorize the request.</response>
    ///<response code="500">Server error. If the server did not understand the request.</response>
    [HttpPut]
    [Route("activate/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Activate(string userId)
    {
        var baseResult = await _service.User.Activate(userId);
        if(!baseResult.Success)
            return ProcessError(baseResult);

        var activationResult = baseResult.GetResult<string>();
        return Ok(activationResult);
    }

    ///<summary>
    ///End-point to deactivate the user profile
    ///</summary>
    ///<param name="userId"></param>
    ///<response code="200">Ok. If everything goes well.</response>
    ///<response code="204">No content. Everything is ok.</response>
    ///<response code="404">Not found. If resource(s) not found.</response>
    ///<response code="400">Bad request. If the request is not valid.</response>
    ///<response code="401">Unauthorized. Invalid authentication credentials for the requested resource.</response>
    ///<response code="403">Forbidden. Server refuses to authorize the request.</response>
    ///<response code="500">Server error. If the server did not understand the request.</response>
    [HttpPut]
    [Route("deactivate/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Deactivate(string userId)
    {
        var baseResult = await _service.User.Deactivate(userId);
        if (!baseResult.Success)
            return ProcessError(baseResult);

        var deactivationResult = baseResult.GetResult<string>();
        return Ok(deactivationResult);
    }

    ///<summary>
    ///End-point to upload user profile photo.
    ///</summary>
    ///<param name="photoToUpload"></param>
    ///<param name="userId"></param>
    ///<response code="200">Ok. If everything goes well.</response>
    ///<response code="204">No content. Everything is ok.</response>
    ///<response code="404">Not found. If resource(s) not found.</response>
    ///<response code="400">Bad request. If the request is not valid.</response>
    ///<response code="401">Unauthorized. Invalid authentication credentials for the requested resource.</response>
    ///<response code="403">Forbidden. Server refuses to authorize the request.</response>
    ///<response code="500">Server error. If the server did not understand the request.</response>
    [HttpPost]
    [Route("upload-photo/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UploadUserPhoto(IFormFile photoToUpload, string userId)
    {
        var baseResult = await _service.User.UploadUserPhoto(photoToUpload, userId);
        if (!baseResult.Success)
            return ProcessError(baseResult);

        var uploadResult = baseResult.GetResult<string>();
        return Ok(uploadResult);
    }

    ///<summary>
    ///End-point to update user profile photo
    ///</summary>
    ///<param name="photoToUpdate"></param>
    ///<param name="userId"></param>
    ///<response code="200">Ok. If everything goes well.</response>
    ///<response code="204">No content. Everything is ok.</response>
    ///<response code="404">Not found. If resource(s) not found.</response>
    ///<response code="400">Bad request. If the request is not valid.</response>
    ///<response code="401">Unauthorized. Invalid authentication credentials for the requested resource.</response>
    ///<response code="403">Forbidden. Server refuses to authorize the request.</response>
    ///<response code="500">Server error. If the server did not understand the request.</response>
    [HttpPut]
    [Route("update-photo/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateUserPhoto(IFormFile photoToUpdate, string userId)
    {
        var baseResult = await _service.User.UpdateUserPhoto(photoToUpdate, userId);
        if (!baseResult.Success)
            return ProcessError(baseResult);

        var updateResult = baseResult.GetResult<string>();
        return Ok(updateResult);
    }

    ///<summary>
    ///End-point to delete user profile photo
    ///</summary>
    ///<param name="userId"></param>
    ///<response code="200">Ok. If everything goes well.</response>
    ///<response code="204">No content. Everything is ok.</response>
    ///<response code="404">Not found. If resource(s) not found.</response>
    ///<response code="400">Bad request. If the request is not valid.</response>
    ///<response code="401">Unauthorized. Invalid authentication credentials for the requested resource.</response>
    ///<response code="403">Forbidden. Server refuses to authorize the request.</response>
    ///<response code="500">Server error. If the server did not understand the request.</response>
    [HttpDelete]
    [Route("delete-photo/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteUserPhoto(string userId)
    {
        var baseResult = await _service.User.RemoveProfilePhoto(userId);
        if (!baseResult.Success)
            return ProcessError(baseResult);

        var deletionResult = baseResult.GetResult<string>();
        return Ok(deletionResult);
    }

    /// <summary>
    /// This end-point changes user's first and last names
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="request"></param>
    ///<response code="200">Ok. If everything goes well.</response>
    ///<response code="204">No content. Everything is ok.</response>
    ///<response code="404">Not found. If resource(s) not found.</response>
    ///<response code="400">Bad request. If the request is not valid.</response>
    ///<response code="401">Unauthorized. Invalid authentication credentials for the requested resource.</response>
    ///<response code="403">Forbidden. Server refuses to authorize the request.</response>
    ///<response code="500">Server error. If the server did not understand the request.</response>
    [HttpPut("edit-names/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateNames(string userId, [FromBody] UserNamesForUpdateDto request)
    {
        var baseResult = await _service.User.UpdateUserNames(userId, request);
        if (!baseResult.Success)
            return ProcessError(baseResult);

        return Ok(baseResult.GetResult<string>());
    }
}