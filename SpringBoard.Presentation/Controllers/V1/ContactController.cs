using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Shared.DataTransferObjects;
using SpringBoard.Presentation.Controllers.V1.Extensions;

namespace SpringBoard.Presentation.Controllers.V1
{
    [ApiController]
    [Route("api/contact")]
    [ApiVersion("1.0")]
    public class ContactController : ApiControllerBase
    {
        private readonly IServiceManager service;
        public ContactController(IServiceManager service) => this.service = service;

        ///<summary>
        ///Creates Contact entity
        ///</summary>
        ///<param name="request"></param>
        ///<returns>Contact object</returns>
        ///<response code="201">Created</response>
        ///<response code="401">Unauthorized</response>
        ///<response code="404">Not Found</response>
        ///<response code="403">Forbidden</response>
        ///<response code="500">Server error</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [Authorize(Roles = "Administrator, SuperAdministrator")]
        public async Task<IActionResult> Create([FromForm] ContactForCreationDto request)
        {
            var baseResult = await service.Contact.Create(request);
            if(!baseResult.Success)
                return ProcessError(baseResult);

            return Created(nameof(Get), baseResult.GetResult<ContactToReturnDto>());
        }

        ///<summary>
        ///Gets Contact entity
        ///</summary>
        ///<returns>Contact object</returns>
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
            return Ok();
        }
    }
}
