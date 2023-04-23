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
/// Dto encargado de contener los datos del vehiculos a dara de alta
/// </summary>
public class CreateVehicleRequest
{
    public int VehicleTypeId { get; set; }

    public int Wheels { get; set; }

    public int BrandId { get; set; }

    public int ModelId { get; set; }

    public string Patent { get; set; }

    public string ChassisNumber { get; set; }

    public long Kilometers { get; set; }
}

public class CreateVehicleResponse : IResponse, IData<VehicleDto>
{
    public Result Result { get; set; }
    public VehicleDto Data { get; set; }
}

#endregion

#region command

public class CreateVehicleCommand : IRequest<CreateVehicleResponse>
{
    public readonly CreateVehicleRequest _dto;

    public CreateVehicleCommand(CreateVehicleRequest dto)
    {
        _dto = dto;
    }
}

public class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand, CreateVehicleResponse>
{
    private readonly ILogger<CreateVehicleCommandHandler> _logger;
    private readonly ISimpleCrudDbContext _dbContext;
    private readonly IValidator<CreateVehicleRequest> _validator;
    private readonly IMapper _mapper;

    public CreateVehicleCommandHandler(ILogger<CreateVehicleCommandHandler> logger
        , ISimpleCrudDbContext dbContext
        , IValidator<CreateVehicleRequest> validator
        , IMapper mapper)
    {
        _logger = logger;
        _dbContext = dbContext;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task<CreateVehicleResponse> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
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

            VehicleType? type =
                await _dbContext.VehicleTypes.FirstOrDefaultAsync(x => x.Id == request._dto.VehicleTypeId,
                    cancellationToken);
            Brand? brand =
                await _dbContext.Brands.FirstOrDefaultAsync(x => x.Id == request._dto.BrandId, cancellationToken);
            Model? model =
                await _dbContext.Models.FirstOrDefaultAsync(x => x.Id == request._dto.ModelId, cancellationToken);

            Vehicle? vehiclePatent =
                await _dbContext.Vehicles.FirstOrDefaultAsync(x => x.Patent.ToUpper() == request._dto.Patent.ToUpper());
            
            Vehicle? vehicleChassisNumber =
                await _dbContext.Vehicles.FirstOrDefaultAsync(x => x.ChassisNumber.ToUpper() == request._dto.ChassisNumber.ToUpper());

            var errors = new List<string>();

            if (type is null)
                errors.Add("No existe el tipo.");
            if (brand is null)
                errors.Add("No existe la marca.");
            if (model is null)
                errors.Add("No existe el modelo.");
            
            if (vehiclePatent is not null)
                errors.Add("Existe un vehículo con la misma patente.");
            
            if (vehicleChassisNumber is not null)
                errors.Add("Existe un vehículo con el mismo número de chasis.");

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

            Vehicle vehicle = new Vehicle
            {
                VehicleTypeId = request._dto.VehicleTypeId,
                Wheels = request._dto.Wheels,
                BrandId = request._dto.BrandId,
                ModelId = request._dto.ModelId,
                Patent = request._dto.Patent.ToUpperInvariant(),
                ChassisNumber = request._dto.ChassisNumber.ToUpperInvariant(),
                Kilometers = request._dto.Kilometers
            };

            await _dbContext.Vehicles.AddAsync(vehicle, cancellationToken);
            var result = await _dbContext.SaveChangesAsync();
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

public class CreateVehicleRequestValidator : AbstractValidator<CreateVehicleRequest>
{
    public CreateVehicleRequestValidator()
    {
        RuleFor(x => x.Kilometers)
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