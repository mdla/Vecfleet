using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Vecfleet.Application.Dtos.Common;
using Vecfleet.Application.Interfaces;
using Vecfleet.Application.Interfaces.Common;
using Vecfleet.Application.Interfaces.Response;
using Vecfleet.Domain.Entities;

namespace Vecfleet.Application.UseCases.Vehicles.Commands;

#region Contracts

/// <summary>
/// Dto encargado de contener los datos del vehiculos a modificar
/// </summary>
public class UpdateVehicleRequest
{
    public int Id { get; set; }
    public int VehicleTypeId { get; set; }

    public int Wheels { get; set; }

    public int BrandId { get; set; }

    public int ModelId { get; set; }

    public string Patent { get; set; }

    public string ChassisNumber { get; set; }

    public long Kilometers { get; set; }
}

public class UpdateVehicleResponse : IResponse, IData<VehicleDto>
{
    public Result Result { get; set; }
    public VehicleDto Data { get; set; }
}

#endregion

#region command

public class UpdateVehicleCommand : IRequest<UpdateVehicleResponse>
{
    public readonly UpdateVehicleRequest _dto;

    public UpdateVehicleCommand(UpdateVehicleRequest dto)
    {
        _dto = dto;
    }
}

public class UpdateVehicleCommandHandler : IRequestHandler<UpdateVehicleCommand, UpdateVehicleResponse>
{
    private readonly ILogger<UpdateVehicleCommandHandler> _logger;
    private readonly ISimpleCrudDbContext _dbContext;
    private readonly IValidator<UpdateVehicleRequest> _validator;
    private readonly IMapper _mapper;

    public UpdateVehicleCommandHandler(ILogger<UpdateVehicleCommandHandler> logger
        , ISimpleCrudDbContext dbContext
        , IValidator<UpdateVehicleRequest> validator
        , IMapper mapper)
    {
        _logger = logger;
        _dbContext = dbContext;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task<UpdateVehicleResponse> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            #region validaciones

            ValidationResult validations = await _validator.ValidateAsync(request._dto, cancellationToken);

            if (!validations.IsValid)
            {
                return new()
                {
                    Result = new Result()
                    {
                        Success = false,
                        Errors = new() { "Error de validación" },
                        Validations = validations.Errors.ToDictionary(x => x.PropertyName, x => x.ErrorMessage)
                    },
                    Data = new()
                };
            }

            Vehicle? vehicle =
                await _dbContext.Vehicles.FirstOrDefaultAsync(x => x.Id == request._dto.Id, cancellationToken);

            VehicleType? type =
                await _dbContext.VehicleTypes.FirstOrDefaultAsync(x => x.Id == request._dto.VehicleTypeId,
                    cancellationToken);
            Brand? brand =
                await _dbContext.Brands.FirstOrDefaultAsync(x => x.Id == request._dto.BrandId, cancellationToken);
            Model? model =
                await _dbContext.Models.FirstOrDefaultAsync(x => x.Id == request._dto.ModelId, cancellationToken);

            var errors = new List<string>();

            if (vehicle is null)
                errors.Add("No existe el vehiculo.");
            if (type is null)
                errors.Add("No existe el tipo.");
            if (brand is null)
                errors.Add("No existe la marca.");
            if (model is null)
                errors.Add("No existe el modelo.");

            if (errors.Any())
                return new()
                {
                    Result = new Result()
                    {
                        Success = false,
                        Errors = errors,
                        Validations = new()
                    },
                    Data = new()
                };

            #endregion

            #region Persistencia

            vehicle.VehicleTypeId = request._dto.VehicleTypeId;
            vehicle.Wheels = request._dto.Wheels;
            vehicle.BrandId = request._dto.BrandId;
            vehicle.ModelId = request._dto.ModelId;
            vehicle.Patent = request._dto.Patent;
            vehicle.ChassisNumber = request._dto.ChassisNumber;
            vehicle.Kilometers = request._dto.Kilometers;

            await _dbContext.SaveChangesAsync();
            return new()
            {
                Result = Result.Successful(),
                Data = _mapper.Map<VehicleDto>(vehicle)
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

public class UpdateVehicleRequestValidator : AbstractValidator<UpdateVehicleRequest>
{
    public UpdateVehicleRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .GreaterThanOrEqualTo(0)
            .WithMessage("Debe tener Id.");
        RuleFor(x => x.Kilometers)
            .NotEmpty()
            .GreaterThanOrEqualTo(0)
            .WithMessage("Los kilometros deben ser mayores a 0.");
        RuleFor(x => x.ChassisNumber)
            .NotEmpty()
            .WithMessage("El número de chasis no puede estar vacio.")
            .MaximumLength(64)
            .WithMessage("El número de chasis no debe contener más de 64 caracteres.");
        RuleFor(x => x.Patent)
            .NotEmpty()
            .WithMessage("La patente no puede estar vacia")
            .MaximumLength(16)
            .WithMessage("La patente no debe contener más de 16 caracteres.");
        RuleFor(x => x.VehicleTypeId)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Seleccione un tipo de vehiculo.");
        RuleFor(x => x.ModelId)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Seleccione un modelo.");
        RuleFor(x => x.BrandId)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Seleccione una marca.");
    }
}

#endregion