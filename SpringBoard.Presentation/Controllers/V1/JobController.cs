using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using SpringBoard.Presentation.Controllers.V1.Extensions;
using System.Security.Claims;

namespace SpringBoard.Presentation.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/job")]
    [ApiController]
    public class JobController : ApiControllerBase
    {
        private readonly IServiceManager _service;

        public JobController(IServiceManager service) => _service = service;

        ///<summary>End-point to create a job</summary>
        ///<param name="request"></param>
        ///<response code="201">Ok. If everything goes well.</response>
        ///<response code="400">Bad request. If the request is not valid.</response>
        ///<response code="401">Unauthorized. Invalid authentication credentials for the requested resource.</response>
        ///<response code="403">Forbidden. Server refuses to authorize the request.</response>
        ///<response code="500">Server error. If the server did not understand the request.</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm]JobRequestObject request)
        {
            var baseResult = await _service.Job.Create(request);
            if(!baseResult.Success)
                return ProcessError(baseResult);

            return Created(nameof(Create), baseResult.GetResult<JobMinimumInfoDto>());
        }

        ///<summary>End-point to create a job</summary>
        ///<param  name="id">The id of the job to update</param>
        ///<param name="request"></param>
        ///<response code="200">Ok. If everything goes well.</response>
        ///<response code="400">Bad request. If the request is not valid.</response>
        ///<response code="401">Unauthorized. Invalid authentication credentials for the requested resource.</response>
        ///<response code="403">Forbidden. Server refuses to authorize the request.</response>
        ///<response code="500">Server error. If the server did not understand the request.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut, Route("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromForm]JobRequestObject request)
        {
            var baseResult = await _service.Job.Update(id, request);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            return Ok(baseResult.GetResult<JobMinimumInfoDto>());
        }

        ///<summary>End-point to create a job</summary>
        ///<param name="id"></param>
        ///<response code="200">Ok. If everything goes well.</response>
        ///<response code="400">Bad request. If the request is not valid.</response>
        ///<response code = "404" > Not.If the resource is not found.</response>
        ///<response code="401">Unauthorized. Invalid authentication credentials for the requested resource.</response>
        ///<response code="403">Forbidden. Server refuses to authorize the request.</response>
        ///<response code="500">Server error. If the server did not understand the request.</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete, Route("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var baseResult = await _service.Job.Delete(id);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            return Ok(baseResult.GetResult<string>());
        }

        ///<summary>End-point to get paginated list of jobs.</summary>
        ///<param name="searchParameters"></param>
        ///<response code="200">Ok. If everything goes well.</response>
        ///<response code="404">Not. If the resource is not found.</response>
        ///<response code="401">Unauthorized. Invalid authentication credentials for the requested resource.</response>
        ///<response code="403">Forbidden. Server refuses to authorize the request.</response>
        ///<response code="500">Server error. If the server did not understand the request.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]SearchParameters searchParameters)
        {
            return Ok((await _service.Job.Get(searchParameters)).GetResult<PaginatedListDto<JobToReturnDto>>());
        }

        ///<summary>End-point to get paginated list of jobs filtered by location, company, job type and industry.</summary>
        ///<param name="searchParameters"></param>
        ///<response code="200">Ok. If everything goes well.</response>
        ///<response code="401">Unauthorized. Invalid authentication credentials for the requested resource.</response>
        ///<response code="403">Forbidden. Server refuses to authorize the request.</response>
        ///<response code="500">Server error. If the server did not understand the request.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("filter")]
        public async Task<IActionResult> Get([FromQuery] JobSearchParams searchParameters)
        {
            return Ok((await _service.Job.Get(searchParameters)).GetResult<PaginatedListDto<JobToReturnDto>>());
        }

        ///<summary>End-point to get paginated list of applicants that applied for a particular job</summary>
        ///<param name="jobId">The id of the job for which the applicants applied for.</param>
        ///<param name="searchParameters"></param>
        ///<response code="200">Ok. If everything goes well.</response>
        ///<response code="401">Unauthorized. Invalid authentication credentials for the requested resource.</response>
        ///<response code="403">Forbidden. Server refuses to authorize the request.</response>
        ///<response code="500">Server error. If the server did not understand the request.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet, Route("{jobId}/applicants")]
        public async Task<IActionResult> GetApplicants(Guid jobId, [FromQuery] SearchParameters searchParameters)
        {
            return Ok((await _service.Job.GetApplicants(jobId, searchParameters)).GetResult<PaginatedListDto<ApplicantInformation>>());
        }

        ///<summary>End-point to get paginated list of jobs posted by a company</summary>
        ///<param name="companyId">The id of the company that posted the job.</param>
        ///<param name="searchParameters"></param>
        ///<response code="200">Ok. If everything goes well.</response>
        ///<response code="401">Unauthorized. Invalid authentication credentials for the requested resource.</response>
        ///<response code="403">Forbidden. Server refuses to authorize the request.</response>
        ///<response code="500">Server error. If the server did not understand the request.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet, Route("company-jobs/{companyId}")]
        public async Task<IActionResult> Get(Guid companyId, [FromQuery] SearchParameters searchParameters)
        {
            return Ok((await _service.Job.Get(companyId, searchParameters)).GetResult<PaginatedListDto<JobToReturnDto>>());
        }

        ///<summary>End-point to get job by id</summary>
        ///<param name="id">The id of the job to get.</param>
        ///<response code="200">Ok. If everything goes well.</response>
        ///<response code="401">Unauthorized. Invalid authentication credentials for the requested resource.</response>
        ///<response code="403">Forbidden. Server refuses to authorize the request.</response>
        ///<response code="500">Server error. If the server did not understand the request.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet, Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok((await _service.Job.Get(id)).GetResult<JobMinimumInfoDto>());
        }

        ///<summary>End-point to get job by by applicant</summary>
        ///<param name="applicantId">The id of the user for which we want to get the job applied for.</param>
        ///<param name="searchParams"></param>
        ///<response code="200">Ok. If everything goes well.</response>
        ///<response code="401">Unauthorized. Invalid authentication credentials for the requested resource.</response>
        ///<response code="403">Forbidden. Server refuses to authorize the request.</response>
        ///<response code="500">Server error. If the server did not understand the request.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet, Route("applicant-jobs/{applicantId}")]
        public async Task<IActionResult> Get(string applicantId, [FromQuery]SearchParameters searchParams)
        {
            return Ok((await _service.Job.Get(applicantId, searchParams)).GetResult<PaginatedListDto<JobToReturnDto>>());
        }

        ///<summary>End point for user to submit job applications</summary>
        ///<param name="jobId">The id of the job the user is applying for</param>
        ///<param name="cv">The CV of the Applicant.</param>
        ///<response code="200">Ok. If everything goes well.</response>
        ///<response code="404">Not found. If resource not found</response>
        ///<response code="401">Unauthorized. Invalid authentication credentials for the requested resource.</response>
        ///<response code="403">Forbidden. Server refuses to authorize the request.</response>
        ///<response code="500">Server error. If the server did not understand the request.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost, Route("apply/{jobId}")]
        [Authorize]
        public async Task<IActionResult> Apply(Guid jobId, IFormFile cv)
        {
            var baseResult = await _service.Job.Apply(jobId, cv);

            if(!baseResult.Success)
                return ProcessError(baseResult);

            return Ok(baseResult.GetResult<string>());
        }
    }
}