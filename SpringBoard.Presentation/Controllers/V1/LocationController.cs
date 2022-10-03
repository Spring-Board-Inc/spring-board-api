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
    [Route("api/location")]
    [ApiController]
    public class LocationController : ApiControllerBase
    {
        private readonly IServiceManager _service;
        public LocationController(IServiceManager service) => _service = service;

        /// <summary>
        /// Creates a new location.
        /// </summary>
        /// <param name="location"></param>
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateLocation([FromBody] LocationForCreationDto location)
        {
            var baseResult = await _service.Location.Create(location);
            if(!baseResult.Success)
                return ProcessError(baseResult);

            return Created(nameof(GetLocation), baseResult.GetResult<LocationDto>());
        }

        /// <summary>
        /// Gets paginated list of locations.
        /// </summary>
        /// <param name="locationParameters"></param>
        /// <returns>List of paginated locations</returns>
        ///<response code="200">Ok. If everything goes well.</response>
        ///<response code="204">No content. Everything is ok.</response>
        ///<response code="404">Not found. If resource(s) not found.</response>
        ///<response code="400">Bad request. If the request is not valid.</response>
        ///<response code="401">Unauthorized. Invalid authentication credentials for the requested resource.</response>
        ///<response code="403">Forbidden. Server refuses to authorize the request.</response>
        ///<response code="500">Server error. If the server did not understand the request.</response>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLocations([FromQuery] SearchParameters locationParameters)
        {
            var baseResult = await _service.Location.Get(locationParameters, trackChanges: true);
            var locations = baseResult.GetResult<PaginatedListDto<LocationDto>>();
            return Ok(locations);
        }

        /// <summary>
        /// Gets a location
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A location with a given id</returns>
        ///<response code="200">Ok. If everything goes well.</response>
        ///<response code="204">No content. Everything is ok.</response>
        ///<response code="404">Not found. If resource(s) not found.</response>
        ///<response code="400">Bad request. If the request is not valid.</response>
        ///<response code="401">Unauthorized. Invalid authentication credentials for the requested resource.</response>
        ///<response code="403">Forbidden. Server refuses to authorize the request.</response>
        ///<response code="500">Server error. If the server did not understand the request.</response>
        [HttpGet, Route("{id:guid}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetLocation(Guid id)
        {
            var baseResult = await _service.Location.Get(id, trackChanges: true);
            if (!baseResult.Success)
                return ProcessError(baseResult);
            
            var location = baseResult.GetResult<LocationDto>();
            return Ok(location);
        }

        /// <summary>
        /// Deletes a location
        /// </summary>
        /// <param name="id"></param>
        /// <returns>No content.</returns>
        ///<response code="200">Ok. If everything goes well.</response>
        ///<response code="204">No content. Everything is ok.</response>
        ///<response code="404">Not found. If resource(s) not found.</response>
        ///<response code="400">Bad request. If the request is not valid.</response>
        ///<response code="401">Unauthorized. Invalid authentication credentials for the requested resource.</response>
        ///<response code="403">Forbidden. Server refuses to authorize the request.</response>
        ///<response code="500">Server error. If the server did not understand the request.</response>
        [HttpDelete, Route("{id:guid}")]
        [Authorize(Roles = "SuperAdministrator, Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteLocation(Guid id)
        {
            var baseResult = await _service.Location.Delete(id, trackChanges: true);
            if(!baseResult.Success)
                return ProcessError(baseResult);

            return Ok(baseResult.GetResult<string>());
        }

        /// <summary>
        /// Updates a location
        /// </summary>
        /// <param name="id"></param>
        /// <param name="location"></param>
        /// <returns>No content</returns>
        ///<response code="200">Ok. If everything goes well.</response>
        ///<response code="204">No content. Everything is ok.</response>
        ///<response code="404">Not found. If resource(s) not found.</response>
        ///<response code="400">Bad request. If the request is not valid.</response>
        ///<response code="401">Unauthorized. Invalid authentication credentials for the requested resource.</response>
        ///<response code="403">Forbidden. Server refuses to authorize the request.</response>
        ///<response code="500">Server error. If the server did not understand the request.</response>
        [HttpPut, Route("{id:guid}")]
        [Authorize(Roles = "SuperAdministrator, Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> UpdateLocation(Guid id, [FromForm] LocationForUpdateDto location)
        {
            var baseResult = await _service.Location.Update(id, location, trackChanges: true);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            return Ok(baseResult.GetResult<string>());
        }
    }
}
