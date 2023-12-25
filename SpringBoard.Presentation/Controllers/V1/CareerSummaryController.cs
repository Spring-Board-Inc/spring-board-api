using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Shared;
using SpringBoard.Presentation.Controllers.V1.Extensions;

namespace SpringBoard.Presentation.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/career-summary")]
    [ApiController]
    public class CareerSummaryController : ApiControllerBase
    {
        private readonly IServiceManager service;

        public CareerSummaryController(IServiceManager service) => this.service = service;

        ///<summary>End-point to create applicants career summary</summary>
        ///<param name="request"></param>
        ///<returns>Created career summary object</returns>
        ///<response code="201">Created</response>
        ///<response code="401">Unauthorized</response>
        ///<response code="403">Forbidden</response>
        ///<response code="500">Server error</response>
        [HttpPost]
        [Authorize(Roles = "Applicant")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(CareerSummaryDto request)
        {
            var userId = service.User.GetUserId();
            var baseResult = await service.CareerSummary.Create(userId, request);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            return Created(nameof(Get), baseResult.GetResult<CareerSummaryReturnDto>());
        }

        ///<summary>End-point to get applicants career summary by user id</summary>
        ///<param name="userId"></param>
        ///<returns>Career summary object</returns>
        ///<response code="200">OK</response>
        ///<response code="401">Unauthorized</response>
        ///<response code="404">Not Found</response>
        ///<response code="403">Forbidden</response>
        ///<response code="500">Server error</response>
        [HttpGet("{userId}")]
        [Authorize(Roles = "Applicant")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(string userId)
        {
            var baseResult = service.CareerSummary.Get(userId);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            return Ok(baseResult.GetResult<CareerSummaryReturnDto>());
        }

        ///<summary>End-point to get all applicants career summary by user id</summary>
        ///<param name="userId"></param>
        ///<returns>List of Career summary object</returns>
        ///<response code="200">OK</response>
        ///<response code="401">Unauthorized</response>
        ///<response code="404">Not Found</response>
        ///<response code="403">Forbidden</response>
        ///<response code="500">Server error</response>
        [HttpGet("all/{userId}")]
        [Authorize(Roles = "Applicant")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetMany(string userId)
        {
            var baseResult = service.CareerSummary.GetMany(userId);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            return Ok(baseResult.GetResult<List<CareerSummaryReturnDto>>());
        }

        ///<summary>End-point to update applicants career summary</summary>
        ///<param name="id"></param>
        ///<param name="request"></param>
        ///<returns>Career summary object</returns>
        ///<response code="200">OK</response>
        ///<response code="401">Unauthorized</response>
        ///<response code="403">Forbidden</response>
        ///<response code="500">Server error</response>
        [HttpPut("{id}")]
        [Authorize(Roles = "Applicant")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(Guid id, CareerSummaryUpdateDto request)
        {
            var userId = service.User.GetUserId();
            var baseResult = await service.CareerSummary.Update(id, userId, request);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            return Ok(baseResult.GetResult<bool>());
        }

        ///<summary>End-point to delete applicants career summary</summary>
        ///<param name="id"></param>
        ///<returns>Career summary object</returns>
        ///<response code="201">Created</response>
        ///<response code="401">Unauthorized</response>
        ///<response code="403">Forbidden</response>
        ///<response code="500">Server error</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Applicant")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = service.User.GetUserId();
            var baseResult = await service.CareerSummary.Delete(id, userId);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            return Ok(baseResult.GetResult<bool>());
        }
    }
}
