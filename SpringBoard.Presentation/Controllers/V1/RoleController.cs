using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects;

namespace SpringBoard.Presentation.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/role")]
    public class RoleController : ApiControllerBase
    {
        private RoleManager<AppRole> _manager;
        public RoleController(RoleManager<AppRole> manager) => _manager = manager;

        ///<summary>
        ///Gets Roles
        ///</summary>
        ///<returns>List of System Roles</returns>
        ///<response code="200">OK</response>
        ///<response code="401">Unauthorized</response>
        ///<response code="404">Not Found</response>
        ///<response code="403">Forbidden</response>
        ///<response code="500">Server error</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[Authorize(Roles = "SuperAdministrator")]
        [HttpGet]
        public IActionResult Get() =>
            Ok(_manager.Roles.Where(_=> true).ToList());

        ///<summary>
        ///Creates System Role
        ///</summary>
        ///<param name="request"></param>
        ///<returns>Identity result</returns>
        ///<response code="201">Created</response>
        ///<response code="401">Unauthorized</response>
        ///<response code="404">Not Found</response>
        ///<response code="403">Forbidden</response>
        ///<response code="500">Server error</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[Authorize(Roles = "SuperAdministrator")]
        [HttpPost]
        public async Task<IActionResult> Create(RoleForCreateDto request)
        {
            var result = await _manager.CreateAsync(new AppRole
            {
                Name = request.RoleName,
                NormalizedName = request.RoleName.ToUpper()
            });

            if(!result.Succeeded)
                return BadRequest(result);

            return Ok(result);
        }

        ///<summary>
        ///Updates System Role
        ///</summary>
        ///<param name="id"></param>
        ///<param name="request"></param>
        ///<returns>Identity result</returns>
        ///<response code="200">OK</response>
        ///<response code="401">Unauthorized</response>
        ///<response code="404">Not Found</response>
        ///<response code="403">Forbidden</response>
        ///<response code="500">Server error</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[Authorize(Roles = "SuperAdministrator")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] RoleForCreateDto request)
        {
            var role = await _manager.FindByIdAsync(id.ToString());
            if (role == null) return NotFound();

            var result = await _manager.UpdateAsync(role);
            if (!result.Succeeded) return BadRequest(result);

            return Ok(result);
        }

        ///<summary>
        ///Permanently deletes system role
        ///</summary>
        ///<param name="id"></param>
        ///<returns>Identity result</returns>
        ///<response code="200">OK</response>
        ///<response code="401">Unauthorized</response>
        ///<response code="404">Not Found</response>
        ///<response code="403">Forbidden</response>
        ///<response code="500">Server error</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[Authorize(Roles = "SuperAdministrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var role = await _manager.FindByIdAsync(id.ToString());
            if(role == null) return NotFound();

            var result = await _manager.DeleteAsync(role);
            if(!result.Succeeded) return BadRequest(result);

            return Ok(result);
        }
    }
}
