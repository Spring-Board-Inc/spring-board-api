using Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using SpringBoard.Presentation.Controllers.V1.Extensions;

namespace SpringBoard.Presentation.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ApiControllerBase
    {
        private readonly IServiceManager _service;

        public AuthenticationController(IServiceManager service) => _service = service;

        /// <summary>
        /// End-point to register new user
        /// </summary>
        /// <param name="userForRegistration">
        /// First Name, Last Name, Password: must be at least, 8 characters including at least,
        /// a special character, a number, upper case and lower case letters.
        /// Confirm password must match the Password. The email must be a valid email address,
        /// because it will have to be confirmed. same with phone number.
        /// Role index is between 0 and 3. Applicant, Company, Admin and SuperAdmin respectively.
        /// </param>
        /// <returns>Idebtity result</returns>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad Requst</response>
        /// <response code="204">No content</response>
        /// <response code="500">Internal server error</response>
        [HttpPost("register")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Register(UserForRegistrationDto userForRegistration)
        {
            var role = ((ERoles)userForRegistration.RoleIndex).ToString();
            var origin = HttpContext.Request.Headers["Origin"];

            var baseResult = await _service.Authentication.RegisterUser(userForRegistration, role, origin);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            var registrationResult = baseResult.GetResult<IdentityResult>();
            return Ok(registrationResult);
        }

        /// <summary>
        /// End-point to confirm user's email address
        /// </summary>
        /// <param name="requestParameters"></param>
        /// <returns>Identity result</returns>
        ///<response code="200">Ok</response>
        ///<response code="404">Not found</response>
        ///<response code="400">Bad request</response>
        ///<response code="500">Server error</response>
        [HttpPost("confirm-email")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ConfirmEmail(EmailConfirmationRequestParameters requestParameters)
        {
            var baseResult = await _service.Authentication.ConfirmEmail(requestParameters);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            return Ok(baseResult.GetResult<IdentityResult>());
        }

        /// <summary>
        /// Logs in users with username (email address) and password
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Access and refresh tokens.</returns>
        ///<response code="200">Ok</response>
        ///<response code="404">Not found</response>
        ///<response code="400">Bad request</response>
        ///<response code="500">Server error</response>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Authenticate(UserForAuthenticationDto user)
        {
            var baseResult = await _service.Authentication.ValidateUser(user);
            if(!baseResult.Success)
                return ProcessError(baseResult);

            var tokenDto = await _service.Authentication.CreateToken(populateExp: true);
            return Ok(tokenDto);
        }

        /// <summary>
        /// This end-point resets user password
        /// </summary>
        /// <param name="resetPasswordDto"></param>
        /// <returns>Ok</returns>
        ///<response code="200">Ok</response>
        ///<response code="400">Bad request</response>
        ///<response code="500">Server error</response>
        [HttpPost("reset-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var origin = HttpContext.Request.Headers["Origin"];
            var baseResult = await _service.Authentication.ResetPassword(resetPasswordDto, origin);
            if(!baseResult.Success)
                return ProcessError(baseResult);

            return Ok(baseResult.GetResult<string>());
        }

        /// <summary>
        /// This end-point changes user forgotten password
        /// </summary>
        /// <param name="changePasswordDto"></param>
        /// <returns>Ok</returns>
        ///<response code="200">Ok</response>
        ///<response code="404">Not found</response>
        ///<response code="400">Bad request</response>
        ///<response code="500">Server error</response>
        [HttpPatch("change-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ChangePassword(ChangeForgottenPasswordDto changePasswordDto)
        {
            var baseResult = await _service.Authentication.ChangeForgottenPassword(changePasswordDto);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            return Ok(baseResult.GetResult<string>());
        }

        /// <summary>
        /// This end-point changes user password when the old password is known.
        /// </summary>
        /// <param name="changePasswordDto"></param>
        /// <returns>Ok</returns>
        ///<response code="200">Ok</response>
        ///<response code="400">Bad request</response>
        ///<response code="401">Unauthorized</response>
        ///<response code="403">Forbidden</response>
        ///<response code="500">Server error</response>
        [HttpPut("change-password")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var baseResult = await _service.Authentication.ChangePassword(changePasswordDto);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            return Ok(baseResult.GetResult<string>());
        }
    }
}