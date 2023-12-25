using Microsoft.AspNetCore.Authorization;
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
    ///End-point to get a user
    ///</summary>
    ///<param name="id"></param>
    ///<returns>User object</returns>
    ///<response code="200">Ok</response>
    ///<response code="404">Not found</response>
    ///<response code="401">Unauthorized</response>
    ///<response code="403">Forbidden</response>
    ///<response code="500">Server error</response>
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get(string id)
    {
        var baseResult = await _service.User.Get(id);
        if (!baseResult.Success)
            return ProcessError(baseResult);

        var result = baseResult.GetResult<DetailedUserToReturnDto>();
        return Ok(result);
    }

    ///<summary>
    ///End-point to get a user (who has in Applicant role) detailed info
    ///</summary>
    ///<param name="id"></param>
    ///<returns>User object</returns>
    ///<response code="200">Ok</response>
    ///<response code="404">Not found</response>
    ///<response code="401">Unauthorized</response>
    ///<response code="403">Forbidden</response>
    ///<response code="500">Server error</response>
    [HttpGet]
    [Route("details/{id}")]
    [Authorize(Roles = "Applicant")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetDetails([FromRoute]Guid id)
    {
        var baseResult = _service.User.GetDetails(id);
        if (!baseResult.Success)
            return ProcessError(baseResult);

        var result = baseResult.GetResult<ApplicantInformation>();
        return Ok(result);
    }

    ///<summary>
    ///End-point to get paginated list of user profile information
    ///</summary>
    ///<returns>Paginated list of user objects</returns>
    ///<param name="searchParameters"></param>
    ///<response code="200">Ok</response>
    ///<response code="401">Unauthorized</response>
    ///<response code="403">Forbidden</response>
    ///<response code="500">Server error</response>
    [HttpGet]
    [Authorize(Roles = "SuperAdministrator, Administrator")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Get([FromQuery] SearchParameters searchParameters)
    {
        var baseResult = _service.User.Get(searchParameters);
        var result = baseResult.GetResult<PaginatedListDto<DetailedUserToReturnDto>>();
        return Ok(result);
    }

    ///<summary>
    ///End-point to activate the user profile
    ///</summary>
    ///<param name="userId"></param>
    ///<returns>Ok</returns>
    ///<response code="200">Ok</response>
    ///<response code="404">Not found</response>
    ///<response code="400">Bad request</response>
    ///<response code="401">Unauthorized</response>
    ///<response code="403">Forbidden</response>
    ///<response code="500">Server error</response>
    [HttpPut]
    [Route("activate/{userId}")]
    [Authorize(Roles = "SuperAdministrator, Administrator, Applicant, Employer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Activate(string userId)
    {
        var baseResult = await _service.User.Activate(userId);
        if(!baseResult.Success)
            return ProcessError(baseResult);

        var activationResult = baseResult.GetResult<bool>();
        return Ok(activationResult);
    }

    ///<summary>
    ///End-point to deactivate the user profile
    ///</summary>
    ///<param name="userId"></param>
    ///<returns>Ok</returns>
    ///<response code="200">Ok</response>
    ///<response code="404">Not found</response>
    ///<response code="400">Bad request</response>
    ///<response code="401">Unauthorized</response>
    ///<response code="403">Forbidden</response>
    ///<response code="500">Server error</response>
    [HttpPut, Route("deactivate/{userId}")]
    [Authorize(Roles = "SuperAdministrator, Administrator, Applicant, Employer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Deactivate(string userId)
    {
        var baseResult = await _service.User.Deactivate(userId);
        if (!baseResult.Success)
            return ProcessError(baseResult);

        var deactivationResult = baseResult.GetResult<bool>();
        return Ok(deactivationResult);
    }

    ///<summary>
    ///End-point to suspend the user profile
    ///This action can only be performed by the Admins
    ///and SuperAdmins
    ///</summary>
    ///<param name="userId"></param>
    ///<returns>Ok</returns>
    ///<response code="200">Ok</response>
    ///<response code="404">Not found</response>
    ///<response code="400">Bad request</response>
    ///<response code="401">Unauthorized</response>
    ///<response code="403">Forbidden</response>
    ///<response code="500">Server error</response>
    [HttpPut, Route("suspend/{userId}")]
    [Authorize(Roles = "SuperAdministrator, Administrator")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Suspend(string userId)
    {
        var baseResult = await _service.User.Suspend(userId);
        if (!baseResult.Success)
            return ProcessError(baseResult);

        var deactivationResult = baseResult.GetResult<bool>();
        return Ok(deactivationResult);
    }

    ///<summary>
    ///End-point to re-activate the user profile
    ///</summary>
    ///<param name="userId"></param>
    ///<returns>Ok</returns>
    ///<response code="200">Ok</response>
    ///<response code="404">Not found</response>
    ///<response code="400">Bad request</response>
    ///<response code="401">Unauthorized</response>
    ///<response code="403">Forbidden</response>
    ///<response code="500">Server error</response>
    [HttpPut]
    [Route("reactivate/{userId}")]
    [Authorize(Roles = "SuperAdministrator, Administrator")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Reactivate(string userId)
    {
        var baseResult = await _service.User.Reactivate(userId);
        if (!baseResult.Success)
            return ProcessError(baseResult);

        var reactivationResult = baseResult.GetResult<bool>();
        return Ok(reactivationResult);
    }

    ///<summary>
    ///End-point to upload user profile photo.
    ///</summary>
    ///<param name="photoToUpload"></param>
    ///<param name="userId"></param>
    ///<returns>Ok</returns>
    ///<response code="200">Ok</response>
    ///<response code="404">Not found</response>
    ///<response code="400">Bad request</response>
    ///<response code="401">Unauthorized</response>
    ///<response code="403">Forbidden</response>
    ///<response code="500">Server error</response>
    [HttpPost]
    [Route("upload-photo/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UploadUserPhoto([FromForm]PhotoToUploadDto photoToUpload,[FromRoute] string userId)
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
    ///<returns>Ok</returns>
    ///<response code="200">Ok</response>
    ///<response code="404">Not found</response>
    ///<response code="400">Bad request</response>
    ///<response code="401">Unauthorized</response>
    ///<response code="403">Forbidden</response>
    ///<response code="500">Server error</response>
    [HttpPut]
    [Route("update-photo/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateUserPhoto([FromForm]PhotoToUploadDto photoToUpdate, [FromRoute]string userId)
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
    ///<returns>Ok</returns>
    ///<response code="200">Ok</response>
    ///<response code="404">Not found</response>
    ///<response code="400">Bad request</response>
    ///<response code="401">Unauthorized</response>
    ///<response code="403">Forbidden</response>
    ///<response code="500">Server error</response>
    [HttpDelete]
    [Route("delete-photo/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteUserPhoto([FromRoute]string userId)
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
    ///<response code="200">Ok</response>
    ///<response code="404">Not found</response>
    ///<response code="400">Bad request</response>
    ///<response code="401">Unauthorized</response>
    ///<response code="403">Forbidden.</response>
    ///<response code="500">Server error</response>
    [HttpPut("edit-names/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateNames(string userId, UserNamesForUpdateDto request)
    {
        var baseResult = await _service.User.UpdateUserNames(userId, request);
        if (!baseResult.Success)
            return ProcessError(baseResult);

        return Ok(baseResult.GetResult<DetailedUserToReturnDto>());
    }

    /// <summary>
    /// This end-point changes user's address details
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="request"></param>
    ///<response code="200">Ok</response>
    ///<response code="404">Not found</response>
    ///<response code="400">Bad request</response>
    ///<response code="401">Unauthorized</response>
    ///<response code="403">Forbidden.</response>
    ///<response code="500">Server error</response>
    [HttpPut("edit-address/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAddress(string userId, UserAddressForUpdateDto request)
    {
        var baseResult = await _service.User.UpdateUserAddress(userId, request);
        if (!baseResult.Success)
            return ProcessError(baseResult);

        return Ok(baseResult.GetResult<DetailedUserToReturnDto>());
    }
}