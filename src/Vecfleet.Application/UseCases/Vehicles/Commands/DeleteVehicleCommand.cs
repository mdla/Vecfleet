using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Vecfleet.Application.Dtos.Common;
using Vecfleet.Application.Interfaces;
using Vecfleet.Application.Interfaces.Common;
using Vecfleet.Application.Interfaces.Request;
using Vecfleet.Application.Interfaces.Response;
using Vecfleet.Domain.Entities;

namespace Vecfleet.Application.UseCases.Vehicles.Commands;

#region Contracts

/// <summary>
/// Dto encargado de contener el id del vehiculo al borrar.
/// </summary>
public class DeleteVehicleRequest :IIdRequest<int>
{
    public int Id { get; set; }
}

public class DeleteVehicleResponse : IResponse, IData<VehicleDto>
{
    public Result Result { get; set; }
    public VehicleDto Data { get; set; }
}

#endregion

#region command

public class DeleteVehicleCommand : IRequest<DeleteVehicleResponse>
{
    public readonly DeleteVehicleRequest _dto;

    public DeleteVehicleCommand(DeleteVehicleRequest dto)
    {
        _dto = dto;
    }
}

public class DeleteVehicleCommandHandler : IRequestHandler<DeleteVehicleCommand, DeleteVehicleResponse>
{
    private readonly ILogger<DeleteVehicleCommandHandler> _logger;
    private readonly ISimpleCrudDbContext _dbContext;
    private readonly IValidator<DeleteVehicleRequest> _validator;
    private readonly IMapper _mapper;

    public DeleteVehicleCommandHandler(ILogger<DeleteVehicleCommandHandler> logger
        , ISimpleCrudDbContext dbContext
        , IValidator<DeleteVehicleRequest> validator
        , IMapper mapper)
    {
        _logger = logger;
        _dbContext = dbContext;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task<DeleteVehicleResponse> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            #region validaciones

            
            Vehicle? vehicle =
                await _dbContext.Vehicles.FirstOrDefaultAsync(x => x.Id == request._dto.Id, cancellationToken);

         

            if (vehicle is null)
                return new()
                {
                    Result = new Result()
                    {
                        Success = false,
                        Errors = new(){"No existe el vehiculo."},
                        Validations = new()
                    },
                    Data = new()
                };

            #endregion

            #region Borrado

            var dto = _mapper.Map<VehicleDto>(vehicle);
            
            _dbContext.Vehicles.Remove(vehicle);

            await _dbContext.SaveChangesAsync();
            return new()
            {
                Result = Result.Successful(),
                Data = dto
            };

            #endregion
        }
        catch (Exception e)
        {
            return new()
            {
                Result = Result.FailWithException(e),
                Data = new()
            };
        }
    }
}

#endregion

#region Validator

public class DeleteVehicleRequestValidator : AbstractValidator<DeleteVehicleRequest>
{
    public DeleteVehicleRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .GreaterThanOrEqualTo(0)
            .WithMessage("Debe tener Id.");
    }
}

#endregion