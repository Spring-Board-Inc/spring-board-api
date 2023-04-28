using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using SpringBoard.Presentation.Controllers.V1.Extensions;

namespace SpringBoard.Presentation.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/skill")]
    [ApiController]
    public class SkillController : ApiControllerBase
    {
        private readonly IServiceManager _service;

        public SkillController(IServiceManager service) => _service = service;

        /// <summary>
        /// Creates a new skill.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Created skill object</returns>
        ///<response code="201">Created</response>
        ///<response code="400">Bad request</response>
        ///<response code="401">Unauthorized</response>
        ///<response code="403">Forbidden</response>
        ///<response code="500">Server error</response>
        [HttpPost]
        [Authorize(Roles = "SuperAdministrator, Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Create(SkillRequest request)
        {
            var baseResult = await _service.Skill.Create(request);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            return Ok(baseResult.GetResult<SkillToReturnDto>());
        }

        /// <summary>
        /// Updates skills.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="id">Id of the skill to be updated.</param>
        /// <returns>Updated skill object</returns>
        ///<response code="200">Ok</response>
        ///<response code="404">Not found</response>
        ///<response code="400">Bad request</response>
        ///<response code="401">Unauthorized</response>
        ///<response code="403">Forbidden</response>
        ///<response code="500">Server error</response>
        [HttpPut, Route("{id}")]
        [Authorize(Roles = "SuperAdministrator, Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Update(Guid id, SkillRequest request)
        {
            var baseResult = await _service.Skill.Update(id, request);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            return Ok(baseResult.GetResult<bool>());
        }

        /// <summary>
        /// Deletes skill.
        /// </summary>
        /// <param name="id">Id of the skill to be deleted.</param>
        /// <returns>Ok</returns>
        ///<response code="200">Ok</response>
        ///<response code="404">Not found</response>
        ///<response code="401">Unauthorized</response>
        ///<response code="403">Forbidden</response>
        ///<response code="500">Server error</response>
        [HttpDelete, Route("{id}")]
        [Authorize(Roles = "SuperAdministrator, Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var baseResult = await _service.Skill.Delete(id);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            return Ok(baseResult.GetResult<bool>());
        }

        /// <summary>
        /// Get list of skill objects
        /// </summary>
        /// <returns>List of skill objects</returns>
        ///<response code="200">Ok</response>
        ///<response code="401">Unauthorized</response>
        ///<response code="403">Forbidden</response>
        ///<response code="500">Server error</response>
        [HttpGet]
        [Authorize(Roles = "SuperAdministrator, Administrator, Applicant")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Get([FromQuery] SearchParameters parameters)
        {
            return Ok((await _service.Skill.Get(parameters)).GetResult<PaginatedListDto<SkillDto>>());
        }

        /// <summary>
        /// Get non-paged list of skill objects
        /// </summary>
        /// <returns>List of skill objects</returns>
        ///<response code="200">Ok</response>
        ///<response code="401">Unauthorized</response>
        ///<response code="403">Forbidden</response>
        ///<response code="500">Server error</response>
        [HttpGet("all")]
        [Authorize(Roles = "SuperAdministrator, Administrator, Applicant")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.Skill.GetAll());
        }

        /// <summary>
        /// End point to get a skill object
        /// </summary>
        /// <param name="id">Id of the skill to be deleted.</param>
        /// <returns>List of skill objects</returns>
        ///<response code="200">Ok</response>
        ///<response code="401">Unauthorized</response>
        ///<response code="403">Forbidden</response>
        ///<response code="500">Server error</response>
        [HttpGet, Route("{id}")]
        [Authorize(Roles = "SuperAdministrator, Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Get(Guid id)
        {
            var baseResult = await _service.Skill.Get(id);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            return Ok(baseResult.GetResult<Skill>());
        }
    }
}