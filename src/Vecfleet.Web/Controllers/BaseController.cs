using Microsoft.AspNetCore.Mvc;
using Vecfleet.Application.Dtos;

namespace Vecfleet.Controllers;

/// <summary>
/// Clase basica de los controladores.
/// </summary>
public class BaseController: ControllerBase
{
    /// <summary>
    /// Maneja los resutlados de la api
    /// </summary>
    /// <param name="result">Result del comando</param> 
    /// <param name="dto">Objeto a devolver</param>
    /// <param name="statusCode">Define el status code cuando resulte es True</param>
    /// <returns>Accion de respuesta para la api</returns>
    protected IActionResult HandleResult(Result result, Object dto,int statusCode = StatusCodes.Status200OK  )
    {
        if (result.Success)
        {
            return StatusCode(statusCode, dto);
        }
        else if (result.Validations.Any())
        {
            return new BadRequestObjectResult(result.Validations);
        }

        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }
}