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

        #region Country Actions
        /// <summary>
        /// End point to create a new country.
        /// </summary>
        /// <param name="country"></param>
        /// <returns>Location object</returns>
        ///<response code="204">No content</response>
        ///<response code="400">Bad request</response>
        ///<response code="401">Unauthorized</response>
        ///<response code="403">Forbidden</response>
        ///<response code="500">Server error</response>
        [HttpPost("country")]
        [Authorize(Roles = "SuperAdministrator, Administrator")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CountryForCreationDto country)
        {
            var baseResult = await _service.Location.CreateCountry(country);
            if(!baseResult.Success)
                return ProcessError(baseResult);

            return Created(nameof(Get), baseResult.GetResult<bool>());
        }

        /// <summary>
        /// End point to get countryby id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Location object</returns>
        ///<response code="200">Ok</response>
        ///<response code="404">Not found</response>
        ///<response code="401">Unauthorized</response>
        ///<response code="403">Forbidden</response>
        ///<response code="500">Server error</response>
        [HttpGet, Route("country/{id}")]
        //[Authorize(Roles = "SuperAdministrator, Administrator")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Get(Guid id)
        {
            var baseResult = await _service.Location.GetCountry(id, trackChanges: false);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            var country = baseResult.GetResult<CountryDto>();
            return Ok(country);
        }

        /// <summary>
        /// End point to get a paginated list of countries.
        /// </summary>
        /// <param name="searchParameters"></param>
        /// <returns>List of paginated country objects</returns>
        ///<response code="200">Ok</response>
        ///<response code="401">Unauthorized</response>
        ///<response code="403">Forbidden</response>
        ///<response code="500">Server error</response>
        [HttpGet("country")]
        //[Authorize(Roles = "SuperAdministrator, Administrator")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromQuery] SearchParameters searchParameters)
        {
            var baseResult = await _service.Location.GetCountries(searchParameters);
            var countries = baseResult.GetResult<PaginatedListDto<CountryDto>>();
            return Ok(countries);
        }

        /// <summary>
        /// Deletes a location
        /// </summary>
        /// <param name="id">the id of the country to delete</param>
        /// <returns>Ok</returns>
        ///<response code="200">Ok</response>
        ///<response code="404">Not found</response>
        ///<response code="401">Unauthorized</response>
        ///<response code="403">Forbidden</response>
        ///<response code="500">Server error</response>
        [HttpDelete, Route("country/{id}")]
        [Authorize(Roles = "SuperAdministrator, Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var baseResult = await _service.Location.DeleteCountry(id, trackChanges: true);
            if(!baseResult.Success)
                return ProcessError(baseResult);

            return Ok(baseResult.GetResult<bool>());
        }

        /// <summary>
        /// End point to update a country
        /// </summary>
        /// <param name="id">The id of the country to update</param>
        /// <param name="country"></param>
        /// <returns>Updated country object</returns>
        ///<response code="200">Ok</response>
        ///<response code="404">Not found</response>
        ///<response code="400">Bad request</response>
        ///<response code="401">Unauthorized</response>
        ///<response code="403">Forbidden</response>
        ///<response code="500">Server error</response>
        [HttpPut, Route("country/{id}")]
        [Authorize(Roles = "SuperAdministrator, Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Update(Guid id, CountryForUpdateDto country)
        {
            var baseResult = await _service.Location.UpdateCountry(id, country, trackChanges: true);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            return Ok(baseResult.GetResult<bool>());
        }
        #endregion

        #region State Actions
        /// <summary>
        /// End point to create a new state.
        /// </summary>
        /// <param name="state"></param>
        /// <returns>True for success, error reponse for failure</returns>
        ///<response code="204">No content</response>
        ///<response code="400">Bad request</response>
        ///<response code="401">Unauthorized</response>
        ///<response code="403">Forbidden</response>
        ///<response code="500">Server error</response>
        [HttpPost("state")]
        [Authorize(Roles = "SuperAdministrator, Administrator")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] StateForCreationDto state)
        {
            var baseResult = await _service.Location.CreateState(state);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            return Created(nameof(Get), baseResult.GetResult<bool>());
        }

        /// <summary>
        /// End point to get state by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>State object</returns>
        ///<response code="200">Ok</response>
        ///<response code="404">Not found</response>
        ///<response code="401">Unauthorized</response>
        ///<response code="403">Forbidden</response>
        ///<response code="500">Server error</response>
        [HttpGet, Route("state/{id}")]
        //[Authorize(Roles = "SuperAdministrator, Administrator")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetSingleState(Guid id)
        {
            var baseResult = await _service.Location.GetState(id, trackChanges: false);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            var state = baseResult.GetResult<StateDto>();
            return Ok(state);
        }

        /// <summary>
        /// End point to get a paginated list of states.
        /// </summary>
        /// <param name="searchParameters"></param>
        /// <returns>List of paginated state objects</returns>
        ///<response code="200">Ok</response>
        ///<response code="401">Unauthorized</response>
        ///<response code="403">Forbidden</response>
        ///<response code="500">Server error</response>
        [HttpGet("state")]
        //[Authorize(Roles = "SuperAdministrator, Administrator")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromQuery] StateSearchParameters searchParameters)
        {
            var baseResult = await _service.Location.GetStates(searchParameters, false);
            var states = baseResult.GetResult<PaginatedListDto<StateDto>>();
            return Ok(states);
        }

        /// <summary>
        /// Deletes a state
        /// </summary>
        /// <param name="id">the id of the state to delete</param>
        /// <returns>Ok</returns>
        ///<response code="200">Ok</response>
        ///<response code="404">Not found</response>
        ///<response code="401">Unauthorized</response>
        ///<response code="403">Forbidden</response>
        ///<response code="500">Server error</response>
        [HttpDelete, Route("state/{id}")]
        [Authorize(Roles = "SuperAdministrator, Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteState(Guid id)
        {
            var baseResult = await _service.Location.DeleteState(id, trackChanges: true);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            return Ok(baseResult.GetResult<bool>());
        }

        /// <summary>
        /// End point to update a state
        /// </summary>
        /// <param name="id">The id of the state to update</param>
        /// <param name="state"></param>
        /// <returns>Updated country object</returns>
        ///<response code="200">Ok</response>
        ///<response code="404">Not found</response>
        ///<response code="400">Bad request</response>
        ///<response code="401">Unauthorized</response>
        ///<response code="403">Forbidden</response>
        ///<response code="500">Server error</response>
        [HttpPut, Route("state/{id}")]
        [Authorize(Roles = "SuperAdministrator, Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Update(Guid id, StateForUpdateDto state)
        {
            var baseResult = await _service.Location.UpdateState(id, state, trackChanges: true);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            return Ok(baseResult.GetResult<bool>());
        }
        #endregion
    }
}