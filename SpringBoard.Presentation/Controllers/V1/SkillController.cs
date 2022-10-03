using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Shared.DataTransferObjects;
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
        /// <returns>Detail of newly created resource</returns>
        ///<response code="200">Ok. If everything goes well.</response>
        ///<response code="204">No content. Everything is ok.</response>
        ///<response code="404">Not found. If resource(s) not found.</response>
        ///<response code="400">Bad request. If the request is not valid.</response>
        ///<response code="401">Unauthorized. Invalid authentication credentials for the requested resource.</response>
        ///<response code="403">Forbidden. Server refuses to authorize the request.</response>
        ///<response code="500">Server error. If the server did not understand the request.</response>
        //[Authorize(Roles = "SuperAdministrator, Administrator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateSkill([FromForm] SkillRequest request)
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
        /// <returns>Detail of newly created resource</returns>
        ///<response code="200">Ok. If everything goes well.</response>
        ///<response code="404">Not found. If resource(s) not found.</response>
        ///<response code="400">Bad request. If the request is not valid.</response>
        ///<response code="401">Unauthorized. Invalid authentication credentials for the requested resource.</response>
        ///<response code="403">Forbidden. Server refuses to authorize the request.</response>
        ///<response code="500">Server error. If the server did not understand the request.</response>
        //[Authorize(Roles = "SuperAdministrator, Administrator")]
        [HttpPut, Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateSkill(Guid id, [FromForm] SkillRequest request)
        {
            var baseResult = await _service.Skill.Update(id, request);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            return Ok(baseResult.GetResult<string>());
        }

        /// <summary>
        /// Deletes skill.
        /// </summary>
        /// <param name="id">Id of the skill to be deleted.</param>
        /// <returns>Detail of newly created resource</returns>
        ///<response code="200">Ok. If everything goes well.</response>
        ///<response code="404">Not found. If resource(s) not found.</response>
        ///<response code="400">Bad request. If the request is not valid.</response>
        ///<response code="401">Unauthorized. Invalid authentication credentials for the requested resource.</response>
        ///<response code="403">Forbidden. Server refuses to authorize the request.</response>
        ///<response code="500">Server error. If the server did not understand the request.</response>
        //[Authorize(Roles = "SuperAdministrator, Administrator")]
        [HttpDelete, Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteSkill(Guid id)
        {
            var baseResult = await _service.Skill.Delete(id);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            return Ok(baseResult.GetResult<string>());
        }

        /// <summary>
        /// Get list of skills.
        /// </summary>
        /// <returns>Detail of newly created resource</returns>
        ///<response code="200">Ok. If everything goes well.</response>
        //[Authorize(Roles = "SuperAdministrator, Administrator")]
        [HttpGet, Route("list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSkills()
        {
            return Ok((await _service.Skill.Get()).GetResult<IEnumerable<Skill>>());
        }
    }
}