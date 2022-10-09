using Entities.Enums;
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
    [Route("api/company")]
    [ApiController]
    public class CompanyController : ApiControllerBase
    {
        private readonly IServiceManager _service;

        public CompanyController(IServiceManager service) => _service = service;

        ///<summary>End-point to create a new company</summary>
        ///<param name="request"></param>
        ///<returns>Created company object</returns>
        ///<response code="201">Created</response>
        ///<response code="401">Unauthorized</response>
        ///<response code="403">Forbidden</response>
        ///<response code="500">Server error</response>
        [HttpPost]
        [Authorize(Roles = "Employer")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromForm]CompanyRequestObject request)
        {
            var userId = _service.User.GetUserId();
            var baseResult = await _service.Company.Create(userId, request);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            return Created(nameof(Get), baseResult.GetResult<CompanyToReturnDto>());
        }

        ///<summary>End-point to update a company</summary>
        ///<param name="id">The id of the company to update.</param>
        ///<param name="request"></param>
        ///<returns>Updated company object</returns>
        ///<response code="200">Ok</response>
        ///<response code="404">Not found</response>
        ///<response code="400">Bad request</response>
        ///<response code="401">Unauthorized</response>
        ///<response code="403">Forbidden</response>
        ///<response code="500">Server error</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "SuperAdministrator, Administrator, Employer")]
        [HttpPut, Route("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromForm] CompanyRequestObject request)
        {
            var baseResult = await _service.Company.Update(id, request);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            return Ok(baseResult.GetResult<CompanyToReturnDto>());
        }

        ///<summary>End-point to delete a company</summary>
        ///<param name="id">The id of the company to delete</param>
        ///<returns>Ok</returns>
        ///<response code="200">Ok</response>
        ///<response code="404">Not found</response>
        ///<response code="401">Unauthorized</response>
        ///<response code="403">Forbidden</response>
        ///<response code="500">Server error</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete, Route("{id}")]
        [Authorize(Roles = "SuperAdministrator, Administrator, Employer")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var baseResult = await _service.Company.Delete(id);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            return Ok(baseResult.GetResult<string>());
        }

        ///<summary>End-point to find a company</summary>
        ///<param name="id">The id of the company to find</param>
        ///<returns>Company object</returns>
        ///<response code="200">Ok</response>
        ///<response code="404">Not found</response>
        ///<response code="401">Unauthorized</response>
        ///<response code="403">Forbidden</response>
        ///<response code="500">Server error</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet, Route("{id}")]
        [Authorize(Roles = "SuperAdministrator, Administrator, Employer")]
        public async Task<IActionResult> Get(Guid id)
        {
            var baseResult = await _service.Company.Get(id);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            return Ok(baseResult.GetResult<CompanyToReturnDto>());
        }

        ///<summary>End-point to get a list of companies</summary>
        ///<returns>List of company object</returns>
        ///<response code="200">Ok</response>
        ///<response code="401">Unauthorized</response>
        ///<response code="403">Forbidden</response>
        ///<response code="500">Server error</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "SuperAdministrator, Administrator, Employer")]
        public async Task<IActionResult> Get([FromQuery]SearchParameters parameters)
        {
            var userId = _service.User.GetUserId();
            bool isInRole = await _service.User.IsInRole(userId, ERoles.Employer.ToString());

            var result = await _service.Company.Get(userId, isInRole, parameters);
            if (!result.Success)
                return ProcessError(result);

            return Ok(result.GetResult<PaginatedListDto<CompanyToReturnDto>>());
        }
    }
}