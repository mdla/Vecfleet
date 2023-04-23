using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Vecfleet.Application.UseCases.Brands.Query;

namespace Vecfleet.Controllers;

[ApiController]
[Route("brand")]
public class BrandController: BaseController
{
    private readonly ISender _mediator;

    public BrandController(ISender mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [SwaggerOperation(Description = "Lista todas las marcas.")]
    [ProducesResponseType(typeof(ListAllBrandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ListAllBrandResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get()
    {
        ListAllBrandResponse result= await _mediator.Send(new ListAllBrandQuery());

        return HandleResult(result.Result, result);
    }
}