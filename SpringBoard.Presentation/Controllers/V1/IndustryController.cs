using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Shared.DataTransferObjects;
using SpringBoard.Presentation.Controllers.V1.Extensions;

namespace SpringBoard.Presentation.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/industry")]
    [ApiController]
    public class IndustryController : ApiControllerBase
    {
        private readonly IServiceManager _service;

        public IndustryController(IServiceManager service) => _service = service;

        ///<summary>
        ///End-point to create an industry
        ///</summary>
        ///<param name="request"></param>
        ///<response code="200">Ok. If everything goes well.</response>
        ///<response code="400">Bad request. If the request is not valid.</response>
        ///<response code="401">Unauthorized. Invalid authentication credentials for the requested resource.</response>
        ///<response code="403">Forbidden. Server refuses to authorize the request.</response>
        ///<response code="500">Server error. If the server did not understand the request.</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> Create(IndustryRequestObject request)
        {
            var baseResult = await _service.Industry.Create(request);
            if(!baseResult.Success)
                return ProcessError(baseResult);

            var result = baseResult.GetResult<IndustryToReturnDto>();
            return Created(nameof(Get), result);
        }

        ///<summary>End-point to get an industry</summary>
        ///<param name="id">The id of the industry to find</param>
        ///<response code="200">Ok. If everything goes well.</response>
        ///<response code="404">Not found. If the resources is not found.</response>
        ///<response code="401">Unauthorized. Invalid authentication credentials for the requested resource.</response>
        ///<response code="403">Forbidden. Server refuses to authorize the request.</response>
        ///<response code="500">Server error. If the server did not understand the request.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet, Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var baseResult = await _service.Industry.Get(id);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            return Ok(baseResult.GetResult<IndustryToReturnDto>());
        }

        ///<summary>End-point to get a list of industries</summary>
        ///<response code="200">Ok. If everything is OK.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.Industry.Get());
        }

        ///<summary>
        ///End-point to delete an industry
        ///</summary>
        ///<param name="id">The id of the industry to delete</param>
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
        public async Task<IActionResult> Delete(Guid id)
        {
            var baseResult = await _service.Industry.Delete(id);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            var result = baseResult.GetResult<string>();
            return Ok(result);
        }

        ///<summary>
        ///End-point to update an industry
        ///</summary>
        ///<param name="id">The id of the industry to update</param>
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
        public async Task<IActionResult> Update(Guid id, IndustryRequestObject request)
        {
            var baseResult = await _service.Industry.Update(id, request);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            var result = baseResult.GetResult<IndustryToReturnDto>();
            return Ok(result);
        }
    }
}
