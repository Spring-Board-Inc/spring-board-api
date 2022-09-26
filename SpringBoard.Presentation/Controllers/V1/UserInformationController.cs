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
    public class UserInformationController : ApiControllerBase
    {
        private readonly IServiceManager _service;

        public UserInformationController(IServiceManager serviceManager) => _service = serviceManager;

        #region User Information
        ///<summary>
        ///End-point to get user information
        ///</summary>
        ///<param name="userId"></param>
        ///<response code="200">Ok. If everything goes well.</response>
        ///<response code="204">No content. Everything is ok.</response>
        ///<response code="404">Not found. If resource(s) not found.</response>
        ///<response code="400">Bad request. If the request is not valid.</response>
        ///<response code="401">Unauthorized. Invalid authentication credentials for the requested resource.</response>
        ///<response code="403">Forbidden. Server refuses to authorize the request.</response>
        ///<response code="500">Server error. If the server did not understand the request.</response>
        [HttpGet, Route("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserInfo(string userId)
        {
            var baseResult = await _service.UserInformation.Get(userId);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            var result = baseResult.GetResult<UserInformation?>();
            return Ok(result);
        }

        ///<summary>
        ///End-point to create user information
        ///</summary>
        ///<param name="userId"></param>
        ///<param name="dto"></param>
        ///<response code="200">Ok. If everything goes well.</response>
        ///<response code="204">No content. Everything is ok.</response>
        ///<response code="404">Not found. If resource(s) not found.</response>
        ///<response code="400">Bad request. If the request is not valid.</response>
        ///<response code="401">Unauthorized. Invalid authentication credentials for the requested resource.</response>
        ///<response code="403">Forbidden. Server refuses to authorize the request.</response>
        ///<response code="500">Server error. If the server did not understand the request.</response>
        [HttpPost, Route("{userId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateUserInfo(string userId, UserInformationDto dto)
        {
            var exists = await _service.UserInformation.Exists(userId);
            if (exists)
                return BadRequest();

            var baseResult = await _service.UserInformation.Create(userId, dto);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            var result = baseResult.GetResult<UserInformationToReturnDto>();
            return Ok(result);
        }

        ///<summary>
        ///End-point to update user information
        ///</summary>
        ///<param name="id"></param>
        ///<param name="dto"></param>
        ///<response code="200">Ok. If everything goes well.</response>
        ///<response code="204">No content. Everything is ok.</response>
        ///<response code="404">Not found. If resource(s) not found.</response>
        ///<response code="400">Bad request. If the request is not valid.</response>
        ///<response code="401">Unauthorized. Invalid authentication credentials for the requested resource.</response>
        ///<response code="403">Forbidden. Server refuses to authorize the request.</response>
        ///<response code="500">Server error. If the server did not understand the request.</response>
        [HttpPut, Route("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUserInfo(Guid id, UserInformationDto dto)
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
        ///<response code="200">Ok. If everything goes well.</response>
        ///<response code="204">No content. Everything is ok.</response>
        ///<response code="404">Not found. If resource(s) not found.</response>
        ///<response code="400">Bad request. If the request is not valid.</response>
        ///<response code="401">Unauthorized. Invalid authentication credentials for the requested resource.</response>
        ///<response code="403">Forbidden. Server refuses to authorize the request.</response>
        ///<response code="500">Server error. If the server did not understand the request.</response>
        [HttpDelete, Route("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        ///<response code="200">Ok. If everything goes well.</response>
        ///<response code="204">No content. Everything is ok.</response>
        ///<response code="404">Not found. If resource(s) not found.</response>
        ///<response code="400">Bad request. If the request is not valid.</response>
        ///<response code="401">Unauthorized. Invalid authentication credentials for the requested resource.</response>
        ///<response code="403">Forbidden. Server refuses to authorize the request.</response>
        ///<response code="500">Server error. If the server did not understand the request.</response>
        [HttpPost, Route("{userInfoId}/education")]
        [Authorize(Roles = "Applicant")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateUserEducation(Guid userInfoId, EducationForCreationDto request)
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
        ///<response code="200">Ok. If everything goes well.</response>
        ///<response code="204">No content. Everything is ok.</response>
        ///<response code="404">Not found. If resource(s) not found.</response>
        ///<response code="400">Bad request. If the request is not valid.</response>
        ///<response code="401">Unauthorized. Invalid authentication credentials for the requested resource.</response>
        ///<response code="403">Forbidden. Server refuses to authorize the request.</response>
        ///<response code="500">Server error. If the server did not understand the request.</response>
        [HttpPatch, Route("{userInfoId}/education/{id}")]
        [Authorize(Roles = "Applicant")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUserEducations(Guid id, EducationForUpdateDto request)
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
        ///<response code="200">Ok. If everything goes well.</response>
        ///<response code="204">No content. Everything is ok.</response>
        ///<response code="404">Not found. If resource(s) not found.</response>
        ///<response code="400">Bad request. If the request is not valid.</response>
        ///<response code="401">Unauthorized. Invalid authentication credentials for the requested resource.</response>
        ///<response code="403">Forbidden. Server refuses to authorize the request.</response>
        ///<response code="500">Server error. If the server did not understand the request.</response>
        [HttpDelete, Route("{userInfoId}/education/{id}")]
        [Authorize(Roles = "Applicant")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        ///<response code="200">Ok. If everything goes well.</response>
        ///<response code="204">No content. Everything is ok.</response>
        ///<response code="400">Bad request. If the request is not valid.</response>
        ///<response code="401">Unauthorized. Invalid authentication credentials for the requested resource.</response>
        ///<response code="403">Forbidden. Server refuses to authorize the request.</response>
        ///<response code="500">Server error. If the server did not understand the request.</response>
        [HttpPost, Route("{userInfoId}/experience")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateWorkExperience(Guid userInfoId, WorkExperienceRequest request)
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
        ///<response code="200">Ok. If everything goes well.</response>
        ///<response code="400">Bad request. If the request is not valid.</response>
        ///<response code="404">Not found. If resource(s) not found.</response>
        ///<response code="401">Unauthorized. Invalid authentication credentials for the requested resource.</response>
        ///<response code="403">Forbidden. Server refuses to authorize the request.</response>
        ///<response code="500">Server error. If the server did not understand the request.</response>
        [HttpPut, Route("{userInfoId}/experience/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateWorkExperience(Guid id, WorkExperienceRequest request)
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
        ///<response code="200">Ok. If everything goes well.</response>
        ///<response code="404">Not found. If resource(s) not found.</response>
        ///<response code="401">Unauthorized. Invalid authentication credentials for the requested resource.</response>
        ///<response code="403">Forbidden. Server refuses to authorize the request.</response>
        ///<response code="500">Server error. If the server did not understand the request.</response>
        [HttpDelete, Route("{userInfoId}/experience/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        ///<response code="200">Ok. If everything goes well.</response>
        ///<response code="204">No content. Everything is ok.</response>
        ///<response code="400">Bad request. If the request is not valid.</response>
        ///<response code="401">Unauthorized. Invalid authentication credentials for the requested resource.</response>
        ///<response code="403">Forbidden. Server refuses to authorize the request.</response>
        ///<response code="500">Server error. If the server did not understand the request.</response>
        [HttpPost, Route("{userInfoId}/skill/{skillId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateUserSkill(Guid userInfoId, Guid skillId, UserSkillRequest request)
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
        ///<response code="200">Ok. If everything goes well.</response>
        ///<response code="204">No content. Everything is ok.</response>
        ///<response code="400">Bad request. If the request is not valid.</response>
        ///<response code="401">Unauthorized. Invalid authentication credentials for the requested resource.</response>
        ///<response code="403">Forbidden. Server refuses to authorize the request.</response>
        ///<response code="500">Server error. If the server did not understand the request.</response>
        [HttpPut, Route("{userInfoId}/skill/{skillId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUserSkill(Guid userInfoId, Guid skillId, UserSkillRequest request)
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
        ///<response code="200">Ok. If everything goes well.</response>
        ///<response code="204">No content. Everything is ok.</response>
        ///<response code="400">Bad request. If the request is not valid.</response>
        ///<response code="401">Unauthorized. Invalid authentication credentials for the requested resource.</response>
        ///<response code="403">Forbidden. Server refuses to authorize the request.</response>
        ///<response code="500">Server error. If the server did not understand the request.</response>
        [HttpDelete, Route("{userInfoId}/skill/{skillId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        ///<response code="200">Ok. If everything goes well.</response>
        ///<response code="204">No content. Everything is ok.</response>
        ///<response code="400">Bad request. If the request is not valid.</response>
        ///<response code="401">Unauthorized. Invalid authentication credentials for the requested resource.</response>
        ///<response code="403">Forbidden. Server refuses to authorize the request.</response>
        ///<response code="500">Server error. If the server did not understand the request.</response>
        [HttpPost, Route("{userInfoId}/certification")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCertification(Guid userInfoId, CertificationRequest request)
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
        ///<response code="200">Ok. If everything goes well.</response>
        ///<response code="204">No content. Everything is ok.</response>
        ///<response code="400">Bad request. If the request is not valid.</response>
        ///<response code="401">Unauthorized. Invalid authentication credentials for the requested resource.</response>
        ///<response code="403">Forbidden. Server refuses to authorize the request.</response>
        ///<response code="500">Server error. If the server did not understand the request.</response>
        [HttpPut, Route("{userInfoId}/certification/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCertification(Guid id, CertificationRequest request)
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
        ///<response code="200">Ok. If everything goes well.</response>
        ///<response code="400">Bad request. If the request is not valid.</response>
        ///<response code="401">Unauthorized. Invalid authentication credentials for the requested resource.</response>
        ///<response code="403">Forbidden. Server refuses to authorize the request.</response>
        ///<response code="500">Server error. If the server did not understand the request.</response>
        [HttpDelete, Route("{userInfoId}/certification/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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