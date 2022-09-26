using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Shared.DataTransferObjects;
using SpringBoard.Presentation.Controllers.V1.Extensions;

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
        public async Task<IActionResult> Create(JobRequestObject request)
        {
            var baseResult = await _service.Job.Create(request);
            if(!baseResult.Success)
                return ProcessError(baseResult);

            return Created(nameof(Create), baseResult.GetResult<JobToReturnDto>());
        }
    }
}
