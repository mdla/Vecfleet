using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Vecfleet.Application.UseCases.Brands.Query;
using Vecfleet.Application.UseCases.Models.Query;

namespace Vecfleet.Controllers;

[ApiController]
[Route("model")]
public class ModelController: BaseController
{
    private readonly ISender _mediator;

    public ModelController(ISender mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [SwaggerOperation(Description = "Lista los modelos de una marca.")]
    [ProducesResponseType(typeof(FilterModelByBrandQueryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(FilterModelByBrandQueryResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get([FromQuery]FilterModelByBrandQueryRequest request)
    {
        FilterModelByBrandQueryResponse result= await _mediator.Send(new FilterModelByBrandQuery(request));

        return HandleResult(result.Result, result);
    }
}