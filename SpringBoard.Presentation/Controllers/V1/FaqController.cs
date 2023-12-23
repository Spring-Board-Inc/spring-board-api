using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using SpringBoard.Presentation.Controllers.V1.Extensions;

namespace SpringBoard.Presentation.Controllers.V1
{
    [ApiController]
    [Route("api/faq")]
    [ApiVersion("1.0")]
    public class FaqController : ApiControllerBase
    {
        private readonly IServiceManager service;

        public FaqController(IServiceManager service) => this.service = service;

        ///<summary>
        ///Creates FAQ entity
        ///</summary>
        ///<param name="request"></param>
        ///<returns>Created FAQ Object</returns>
        ///<reponse code="201">Created</reponse>
        ///<response code="401">Unathorized</response>
        ///<response code="400">Bad request</response>
        ///<response code="500">Server error</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FaqForCreationDto request)
        {
            var baseResult = await service.Faq.Create(request);
            if(!baseResult.Success)
                return ProcessError(baseResult);

            return Created(nameof(Get), baseResult.GetResult<FaqToReturnDto>());
        }

        ///<summary>
        ///Gets a FAQ entity
        ///</summary>
        ///<param name="id"></param>
        ///<returns>FAQ Object</returns>
        ///<reponse code="200">OK</reponse>
        ///<response code="401">Unathorized</response>
        ///<response code="404">Not found</response>
        ///<response code="400">Bad request</response>
        ///<response code="500">Server error</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var baseResult = await service.Faq.Get(id);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            return Ok(baseResult.GetResult<FaqToReturnDto>());
        }

        ///<summary>
        ///Gets a paginated list of FAQs
        ///</summary>
        ///<param name="parameters"></param>
        ///<returns>List of FAQ objects</returns>
        ///<reponse code="200">OK</reponse>
        ///<response code="401">Unathorized</response>
        ///<response code="500">Server error</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public IActionResult Get([FromQuery] SearchParameters parameters) => Ok(service.Faq.Get(parameters));

        ///<summary>
        ///Updates a FAQ entity
        ///</summary>
        ///<param name="id"></param>
        ///<param name="request"></param>
        ///<returns>Updated FAQ Object</returns>
        ///<reponse code="200">OK</reponse>
        ///<response code="401">Unathorized</response>
        ///<response code="404">Not found</response>
        ///<response code="400">Bad request</response>
        ///<response code="500">Server error</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, FaqForUpdateDto request)
        {
            var baseResult = await service.Faq.Update(id, request);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            return Ok(baseResult.GetResult<FaqToReturnDto>());
        }

        ///<summary>
        ///Deletes a FAQ entity
        ///</summary>
        ///<param name="id"></param>
        ///<returns>True or False</returns>
        ///<reponse code="200">OK</reponse>
        ///<response code="401">Unathorized</response>
        ///<response code="404">Not found</response>
        ///<response code="400">Bad request</response>
        ///<response code="500">Server error</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var baseResult = await service.Faq.Delete(id);
            if (!baseResult.Success)
                return ProcessError(baseResult);

            return Ok(baseResult.GetResult<bool>());
        }
    }
}
