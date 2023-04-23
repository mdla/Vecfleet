using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Vecfleet.Application.UseCases.Brands.Query;
using Vecfleet.Application.UseCases.VehicleTypes.Query;

namespace Vecfleet.Controllers;

[ApiController]
[Route("vehicletype")]
public class VehicleTypeController: BaseController
{
    private readonly ISender _mediator;

    public VehicleTypeController(ISender mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [SwaggerOperation(Description = "Lista todos los tipos de vehiculos.")]
    [ProducesResponseType(typeof(ListAllVehicleTypesResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ListAllVehicleTypesResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get()
    {
        ListAllVehicleTypesResponse result= await _mediator.Send(new ListAllVehicleTypesQuery());

        return HandleResult(result.Result, result);
    }
}