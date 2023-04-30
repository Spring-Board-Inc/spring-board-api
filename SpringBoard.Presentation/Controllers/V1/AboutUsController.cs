using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Shared.DataTransferObjects;
using SpringBoard.Presentation.Controllers.V1.Extensions;

namespace SpringBoard.Presentation.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/about")]
    public class AboutUsController : ApiControllerBase
    {
        private IServiceManager _service;
        public AboutUsController(IServiceManager service) => _service = service;

        ///<summary>
        ///Gets About Us object
        ///</summary>
        ///<returns>About Us object</returns>
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
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var baseResult = await _service.AboutUs.Get();
            if(!baseResult.Success)
                return ProcessError(baseResult);

            return Ok(baseResult.GetResult<AboutUsToReturnDto>());
        }

        ///<summary>
        ///Gets About Us object by id
        ///</summary>
        ///<returns>About Us object</returns>
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
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator, SuperAdministrator")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var baseResult = await _service.AboutUs.Get(id);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            return Ok(baseResult.GetResult<AboutUsToReturnDto>());
        }

        ///<summary>
        ///Creates About Us entity
        ///</summary>
        ///<param name="request"></param>
        ///<returns>About Us object</returns>
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
        [Authorize(Roles = "Administrator, SuperAdministrator")]
        [HttpPost]
        public async Task<IActionResult> Create(AboutUsForCreateDto request)
        {
            var baseResult = await _service.AboutUs.Create(request);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            return Created(nameof(Get), baseResult.GetResult<AboutUsToReturnDto>());
        }

        ///<summary>
        ///Updates About Us entity
        ///</summary>
        ///<param name="id"></param>
        ///<param name="request"></param>
        ///<returns>About Us object</returns>
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
        [Authorize(Roles = "Administrator, SuperAdministrator")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody] AboutUsForUpdateDto request)
        {
            var baseResult = await _service.AboutUs.Update(id, request);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            return Ok(baseResult.GetResult<AboutUsToReturnDto>());
        }

        ///<summary>
        ///Permanently deletes an About Us entity
        ///</summary>
        ///<param name="id"></param>
        ///<returns>True or false</returns>
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
        [Authorize(Roles = "Administrator, SuperAdministrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var baseResult = await _service.AboutUs.Delete(id);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            return Ok(baseResult.GetResult<bool>());
        }

        ///<summary>
        ///Change the IsDeprecated flag of an About Us entity
        ///</summary>
        ///<param name="id"></param>
        ///<returns>True or false</returns>
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
        [Authorize(Roles = "Administrator, SuperAdministrator")]
        [HttpPatch("deprecate/{id}")]
        public async Task<IActionResult> Deprecate([FromRoute] Guid id)
        {
            var baseResult = await _service.AboutUs.Deprecate(id);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            return Ok(baseResult.GetResult<bool>());
        }
    }
}
