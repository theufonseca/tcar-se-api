using Application.UseCases;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IMediator mediator;

        public SearchController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("basic")]
        public async Task<IActionResult> Search(BasicSearchVehicleRequest basicSearchVehicleRequest)
        {
            var result = await mediator.Send(basicSearchVehicleRequest);
            return Ok(result);
        }

        [HttpPost("id")]
        public async Task<IActionResult> SearchById(SearchVehicleByIdRequest searchVehicleByIdRequest)
        {
            var result = await mediator.Send(searchVehicleByIdRequest);
            return Ok(result);
        }

        [HttpPost("fulltext")]
        public async Task<IActionResult> FullTextSearch(FullTextSearchVehicleRequest fullTextSearchVehicleRequest)
        {
            var result = await mediator.Send(fullTextSearchVehicleRequest);
            return Ok(result);
        }
    }
}
