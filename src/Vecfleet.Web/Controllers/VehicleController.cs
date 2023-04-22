using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Vecfleet.Application.Brands.Commands;

namespace Vecfleet.Controllers;

[ApiController]
public class VehicleController: BaseController
{
    private readonly ISender _mediator;

    public VehicleController(ISender mediator)
    {
        _mediator = mediator;
    }


    [HttpPost()]
    [SwaggerOperation(Description = "Crea un vehiculo.")]
    [ProducesResponseType(typeof(CreateVehicleResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Dictionary<string,string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CreateVehicleResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create(CreateVehicleRequest request)
    {
        CreateVehicleResponse result= await _mediator.Send(new CreateVehicleCommand(request));

        return HandleResult(result.Result, result, StatusCodes.Status201Created);

    }
}