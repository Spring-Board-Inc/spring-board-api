using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Shared.DataTransferObjects;
using SpringBoard.Presentation.Controllers.V1.Extensions;

namespace SpringBoard.Presentation.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/jobtype")]
    [ApiController]
    public class JobTypeController : ApiControllerBase
    {
        private readonly IServiceManager _service;
        public JobTypeController(IServiceManager service) => _service = service;

        ///<summary>
        ///End-point to create job type
        ///</summary>
        ///<param name="request"></param>
        ///<response code="200">Ok. If everything goes well.</response>
        ///<response code="404">Not found. If resource(s) not found.</response>
        ///<response code="400">Bad request. If the request is not valid.</response>
        ///<response code="401">Unauthorized. Invalid authentication credentials for the requested resource.</response>
        ///<response code="403">Forbidden. Server refuses to authorize the request.</response>
        ///<response code="500">Server error. If the server did not understand the request.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateJobType([FromForm]JobTypeRequestObject request)
        {
            var baseResult = await _service.JobType.Create(request);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            var result = baseResult.GetResult<JobTypeToReturnDto>();
            return Ok(result);
        }

        ///<summary>
        ///End-point to get list of job types
        ///</summary>
        [HttpGet]
        public async Task<IActionResult> GetJobTypes()
        {
            var result = await _service.JobType.Get();
            return Ok(result);
        }

        ///<summary>
        ///End-point to get a job type
        ///</summary>
        ///<param name="id">The id of the job type to find</param>
        ///<response code="200">Ok. If everything goes well.</response>
        ///<response code="404">Not found. If resource(s) not found.</response>
        ///<response code="400">Bad request. If the request is not valid.</response>
        ///<response code="401">Unauthorized. Invalid authentication credentials for the requested resource.</response>
        ///<response code="403">Forbidden. Server refuses to authorize the request.</response>
        ///<response code="500">Server error. If the server did not understand the request.</response>
        [HttpGet, Route("{id}")]
        public async Task<IActionResult> GetJobType(Guid id)
        {
            var baseResult = await _service.JobType.Get(id);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            var result = baseResult.GetResult<JobTypeToReturnDto>();
            return Ok(result);
        }

        ///<summary>
        ///End-point to delete a job type
        ///</summary>
        ///<param name="id">The id of the job type to delete</param>
        ///<response code="200">Ok. If everything goes well.</response>
        ///<response code="404">Not found. If resource(s) not found.</response>
        ///<response code="400">Bad request. If the request is not valid.</response>
        ///<response code="401">Unauthorized. Invalid authentication credentials for the requested resource.</response>
        ///<response code="403">Forbidden. Server refuses to authorize the request.</response>
        ///<response code="500">Server error. If the server did not understand the request.</response>
        [HttpDelete, Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteJobType(Guid id)
        {
            var baseResult = await _service.JobType.Delete(id);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            var result = baseResult.GetResult<string>();
            return Ok(result);
        }

        ///<summary>
        ///End-point to update a job type
        ///</summary>
        ///<param name="id">The id of the job type to update</param>
        ///<param name="request"></param>
        ///<response code="200">Ok. If everything goes well.</response>
        ///<response code="404">Not found. If resource(s) not found.</response>
        ///<response code="400">Bad request. If the request is not valid.</response>
        ///<response code="401">Unauthorized. Invalid authentication credentials for the requested resource.</response>
        ///<response code="403">Forbidden. Server refuses to authorize the request.</response>
        ///<response code="500">Server error. If the server did not understand the request.</response>
        [HttpPut, Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateJobType(Guid id, [FromForm] JobTypeRequestObject request)
        {
            var baseResult = await _service.JobType.Update(id, request);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            var result = baseResult.GetResult<JobTypeToReturnDto>();
            return Ok(result);
        }
    }
}
