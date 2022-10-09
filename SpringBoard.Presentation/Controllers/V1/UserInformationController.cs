using Entities.Enums;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Shared.DataTransferObjects;
using SpringBoard.Presentation.Controllers.V1.Extensions;

namespace SpringBoard.Presentation.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/user-info")]
    [ApiController]
    [Authorize]
    public class UserInformationController : ApiControllerBase
    {
        private readonly IServiceManager _service;

        public UserInformationController(IServiceManager serviceManager) => _service = serviceManager;

        #region User Information
        ///<summary>
        ///End-point to get user information by user id
        ///</summary>
        ///<param name="userId">The user id</param>
        ///<returns>User information object</returns>
        ///<response code="200">Ok</response>
        ///<response code="404">Not found</response>
        ///<response code="400">Bad request</response>
        ///<response code="401">Unauthorized</response>
        ///<response code="403">Forbidden</response>
        ///<response code="500">Server error.</response>
        [HttpGet, Route("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserInfo(string userId)
        {
            var baseResult = await _service.UserInformation.Get(userId);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            var result = baseResult.GetResult<UserInformationToReturn?>();
            return Ok(result);
        }

        ///<summary>
        ///End-point to create a new user information
        ///</summary>
        ///<param name="userId"></param>
        ///<param name="dto"></param>
        ///<returns>Created user info object</returns>
        ///<response code="201">Created</response>
        ///<response code="404">Not found</response>
        ///<response code="400">Bad request</response>
        ///<response code="401">Unauthorized.</response>
        ///<response code="403">Forbidden</response>
        ///<response code="500">Server error</response>
        [HttpPost, Route("{userId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateUserInfo(string userId, [FromForm]UserInformationDto dto)
        {
            var exists = await _service.UserInformation.Exists(userId);
            if (exists)
                return BadRequest();

            var baseResult = await _service.UserInformation.Create(userId, dto);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            var result = baseResult.GetResult<UserInformationToReturnDto>();
            return Created(nameof(GetUserInfo), result);
        }

        ///<summary>
        ///End-point to update user information
        ///</summary>
        ///<param name="id">The id of the user info object to update</param>
        ///<param name="dto"></param>
        ///<returns>Updated user info object</returns>
        ///<response code="200">Ok</response>
        ///<response code="404">Not found</response>
        ///<response code="400">Bad request</response>
        ///<response code="401">Unauthorized</response>
        ///<response code="403">Forbidden</response>
        ///<response code="500">Server error</response>
        [HttpPut, Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUserInfo(Guid id, [FromForm]UserInformationDto dto)
        {
            var baseResult = await _service.UserInformation.Update(id, dto);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            var result = baseResult.GetResult<string>();
            return Ok(result);
        }

        ///<summary>
        ///End-point to delete user information
        ///</summary>
        ///<param name="id">The id of the user info to delete</param>
        ///<returns>Ok</returns>
        ///<response code="200">Ok</response>
        ///<response code="404">Not found</response>
        ///<response code="401">Unauthorized</response>
        ///<response code="403">Forbidden</response>
        ///<response code="500">Server error</response>
        [HttpDelete, Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteUserInfo(Guid id)
        {
            var baseResult = await _service.UserInformation.Delete(id);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            var result = baseResult.GetResult<string>();
            return Ok(result);
        }

        #endregion

        #region User Education

        ///<summary>
        ///End-point to create education profile for users
        ///</summary>
        ///<param name="userInfoId"></param>
        ///<param name="request"></param>
        ///<returns>Created education object</returns>
        ///<response code="200">Ok</response>
        ///<response code="404">Not found</response>
        ///<response code="400">Bad request</response>
        ///<response code="401">Unauthorized</response>
        ///<response code="403">Forbidden</response>
        ///<response code="500">Server error</response>
        [HttpPost, Route("{userInfoId}/education")]
        [Authorize(Roles = "Applicant")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateUserEducation(Guid userInfoId, [FromForm]EducationForCreationDto request)
        {
            var baseResult = await _service.Education.Create(userInfoId, request);
            if(!baseResult.Success)
                return ProcessError(baseResult);

            var result = baseResult.GetResult<EducationToReturnDto>();
            return Ok(result);
        }

        ///<summary>
        ///End-point to update education profile for a user
        ///</summary>
        ///<param name="id"></param>
        ///<param name="request"></param>
        ///<returns>Ok</returns>
        ///<response code="200">Ok</response>
        ///<response code="404">Not found</response>
        ///<response code="400">Bad request</response>
        ///<response code="401">Unauthorized</response>
        ///<response code="403">Forbidden</response>
        ///<response code="500">Server error</response>
        [HttpPatch, Route("{userInfoId}/education/{id}")]
        [Authorize(Roles = "Applicant")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUserEducations(Guid id, [FromForm]EducationForUpdateDto request)
        {
            var baseResult = await _service.Education.Update(id, request);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            var result = baseResult.GetResult<string>();
            return Ok(result);
        }

        ///<summary>
        ///End-point to delete education profile for a user
        ///</summary>
        ///<param name="id"></param>
        ///<returns>Ok</returns>
        ///<response code="200">Ok</response>
        ///<response code="400">Bad request</response>
        ///<response code="401">Unauthorized</response>
        ///<response code="403">Forbidden</response>
        ///<response code="500">Server error</response>
        [HttpDelete, Route("{userInfoId}/education/{id}")]
        [Authorize(Roles = "Applicant")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteUserEducations(Guid id)
        {
            var baseResult = await _service.Education.Delete(id);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            var result = baseResult.GetResult<string>();
            return Ok(result);
        }

        #endregion

        #region User Work Experience

        ///<summary>
        ///End-point to create user work experience
        ///</summary>
        ///<param name="userInfoId"></param>
        ///<param name="request"></param>
        ///<returns>Work experience object</returns>
        ///<response code="200">Ok</response>
        ///<response code="400">Bad request</response>
        ///<response code="401">Unauthorized</response>
        ///<response code="403">Forbidden</response>
        ///<response code="500">Server error</response>
        [HttpPost, Route("{userInfoId}/experience")]
        [Authorize(Roles = "Applicant")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateWorkExperience(Guid userInfoId, [FromForm]WorkExperienceRequest request)
        {
            var baseResult = await _service.WorkExperience.Create(userInfoId, request);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            var result = baseResult.GetResult<WorkExperienceToReturnDto>();
            return Ok(result);
        }


        ///<summary>
        ///End-point to update user work experience
        ///</summary>
        ///<param name="id">This is the work experience unique id</param>
        ///<param name="request"></param>
        ///<returns>Ok</returns>
        ///<response code="200">Ok</response>
        ///<response code="400">Bad request</response>
        ///<response code="404">Not found</response>
        ///<response code="401">Unauthorized</response>
        ///<response code="403">Forbidden</response>
        ///<response code="500">Server error</response>
        [HttpPut, Route("{userInfoId}/experience/{id}")]
        [Authorize(Roles = "Applicant")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateWorkExperience(Guid id, [FromForm]WorkExperienceRequest request)
        {
            var baseResult = await _service.WorkExperience.Update(id, request);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            var result = baseResult.GetResult<string>();
            return Ok(result);
        }

        ///<summary>
        ///End-point to delete user work experience
        ///</summary>
        ///<param name="id">This is the work experience unique id</param>
        ///<response code="200">Ok</response>
        ///<response code="404">Not found</response>
        ///<response code="401">Unauthorized</response>
        ///<response code="403">Forbidden</response>
        ///<response code="500">Server error</response>
        [HttpDelete, Route("{userInfoId}/experience/{id}")]
        [Authorize(Roles = "Applicant")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteWorkExperience(Guid id)
        {
            var baseResult = await _service.WorkExperience.Delete(id);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            var result = baseResult.GetResult<string>();
            return Ok(result);
        }
        #endregion

        #region User Skill

        ///<summary>
        ///End-point to create user skill
        ///</summary>
        ///<param name="userInfoId"></param>
        ///<param name="skillId"></param>
        ///<param name="request"></param>
        ///<returns>User skill object</returns>
        ///<response code="200">Ok</response>
        ///<response code="400">Bad request</response>
        ///<response code="404">Not found</response>
        ///<response code="401">Unauthorized</response>
        ///<response code="403">Forbidden</response>
        ///<response code="500">Server error</response>
        [HttpPost, Route("{userInfoId}/skill/{skillId}")]
        [Authorize(Roles = "Applicant")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateUserSkill(Guid userInfoId, Guid skillId, [FromForm]UserSkillRequest request)
        {
            var baseResult = await _service.UserSkill.Create(userInfoId, skillId, request);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            var result = baseResult.GetResult<UserSkill>();
            return Ok(result);
        }

        ///<summary>
        ///End-point to update user skill
        ///</summary>
        ///<param name="userInfoId"></param>
        ///<param name="skillId"></param>
        ///<param name="request"></param>
        ///<returns>Ok</returns>
        ///<response code="200">Ok</response>
        ///<response code="400">Bad request</response>
        ///<response code="401">Unauthorized</response>
        ///<response code="404">Not found</response>
        ///<response code="403">Forbidden</response>
        ///<response code="500">Server error</response>
        [HttpPut, Route("{userInfoId}/skill/{skillId}")]
        [Authorize(Roles = "Applicant")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUserSkill(Guid userInfoId, Guid skillId, [FromForm]UserSkillRequest request)
        {
            var baseResult = await _service.UserSkill.Update(userInfoId, skillId, request);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            var result = baseResult.GetResult<string>();
            return Ok(result);
        }

        ///<summary>
        ///End-point to delete user skill
        ///</summary>
        ///<param name="userInfoId"></param>
        ///<param name="skillId"></param>
        ///<returns>Ok</returns>
        ///<response code="200">Ok</response>
        ///<response code="404">Not found</response>
        ///<response code="401">Unauthorized</response>
        ///<response code="403">Forbidden</response>
        ///<response code="500">Server error</response>
        [HttpDelete, Route("{userInfoId}/skill/{skillId}")]
        [Authorize(Roles = "Applicant")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteUserSkill(Guid userInfoId, Guid skillId)
        {
            var baseResult = await _service.UserSkill.Delete(userInfoId, skillId);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            var result = baseResult.GetResult<string>();
            return Ok(result);
        }
        #endregion

        #region Certification

        ///<summary>
        ///End-point to create user certification
        ///</summary>
        ///<param name="userInfoId"></param>
        ///<param name="request"></param>
        ///<returns>Certification object</returns>
        ///<response code="200">Ok</response>
        ///<response code="400">Bad request</response>
        ///<response code="401">Unauthorized</response>
        ///<response code="403">Forbidden</response>
        ///<response code="404">Not found</response>
        ///<response code="500">Server error</response>
        [HttpPost, Route("{userInfoId}/certification")]
        [Authorize(Roles = "Applicant")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCertification(Guid userInfoId, [FromForm]CertificationRequest request)
        {
            var baseResult = await _service.Certification.Create(userInfoId, request);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            var result = baseResult.GetResult<CertificationDto>();
            return Ok(result);
        }

        ///<summary>
        ///End-point to update user certification
        ///</summary>
        ///<param name="id"></param>
        ///<param name="request"></param>
        ///<returns>Ok</returns>
        ///<response code="200">Ok</response>
        ///<response code="404">Not found.</response>
        ///<response code="400">Bad request</response>
        ///<response code="401">Unauthorized</response>
        ///<response code="403">Forbidden</response>
        ///<response code="500">Server error</response>
        [HttpPut, Route("{userInfoId}/certification/{id}")]
        [Authorize(Roles = "Applicant")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCertification(Guid id, [FromForm]CertificationRequest request)
        {
            var baseResult = await _service.Certification.Update(id, request);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            var result = baseResult.GetResult<string>();
            return Ok(result);
        }

        ///<summary>
        ///End-point to create user certification
        ///</summary>
        ///<param name="id"></param>
        ///<returns>Ok</returns>
        ///<response code="200">Ok</response>
        ///<response code="404">Not found</response>
        ///<response code="401">Unauthorized</response>
        ///<response code="403">Forbidden</response>
        ///<response code="500">Server error</response>
        [HttpDelete, Route("{userInfoId}/certification/{id}")]
        [Authorize(Roles = "Applicant")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCertification(Guid id)
        {
            var baseResult = await _service.Certification.Delete(id);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            var result = baseResult.GetResult<string>();
            return Ok(result);
        }
        #endregion
    }
}