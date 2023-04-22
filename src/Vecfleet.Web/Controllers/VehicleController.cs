using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Vecfleet.Application.UseCases.Vehicles.Commands;
using Vecfleet.Application.UseCases.Vehicles.Query;

namespace Vecfleet.Controllers;

[ApiController]
[Route("vehicle")]
public class VehicleController: BaseController
{
    private readonly ISender _mediator;

    public VehicleController(ISender mediator)
    {
        _mediator = mediator;
    }

    
    [HttpGet]
    [SwaggerOperation(Description = "Filtra todos las vehiculos.")]
    [ProducesResponseType(typeof(ListAllVehicleByFilterResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ListAllVehicleByFilterResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get([FromQuery]ListAllVehicleByFilterRequest request)
    {
        ListAllVehicleByFilterResponse result= await _mediator.Send(new ListAllVehicleByFilterQuery(request));

        return HandleResult(result.Result, result);
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
    
    
    [HttpPut]
    [SwaggerOperation(Description = "Actualiza un vehiculo.")]
    [ProducesResponseType(typeof(UpdateVehicleResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Dictionary<string,string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(UpdateVehicleResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Udpate(UpdateVehicleRequest request)
    {
        UpdateVehicleResponse result= await _mediator.Send(new UpdateVehicleCommand(request));

        return HandleResult(result.Result, result);
    }
    
    
    [HttpDelete]
    [SwaggerOperation(Description = "Borra un vehiculo.")]
    [ProducesResponseType(typeof(DeleteVehicleResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Dictionary<string,string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(DeleteVehicleResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(DeleteVehicleRequest request)
    {
        DeleteVehicleResponse result= await _mediator.Send(new DeleteVehicleCommand(request));

        return HandleResult(result.Result, result);
    }
}