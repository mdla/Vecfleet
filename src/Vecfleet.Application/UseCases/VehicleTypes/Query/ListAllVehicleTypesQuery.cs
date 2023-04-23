using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Vecfleet.Application.Dtos.Common;
using Vecfleet.Application.Interfaces;
using Vecfleet.Application.Interfaces.Common;
using Vecfleet.Application.Interfaces.Response;

namespace Vecfleet.Application.UseCases.VehicleTypes.Query;

#region Contracts

public class ListAllVehicleTypesResponse : IListDtoResponse<VehicleTypeDto>
{
    public Result Result { get; set; }
    public List<VehicleTypeDto> Data { get; set; }
}

#endregion

#region command

public class ListAllVehicleTypesQuery : IRequest<ListAllVehicleTypesResponse>
{
}

public class ListVehicleTypesQueryHandler : IRequestHandler<ListAllVehicleTypesQuery, ListAllVehicleTypesResponse>
{
    private readonly ILogger<ListVehicleTypesQueryHandler> _logger;
    private readonly ISimpleCrudDbContext _dbContext;
    private readonly IMapper _mapper;

    public ListVehicleTypesQueryHandler(ILogger<ListVehicleTypesQueryHandler> logger, ISimpleCrudDbContext dbContext,
        IMapper mapper)
    {
        _logger = logger;
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ListAllVehicleTypesResponse> Handle(ListAllVehicleTypesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _dbContext.VehicleTypes.OrderBy(x => x.Description).ToListAsync(cancellationToken);
            
            return new()
            {
                Result = Result.Successful(),
                Data = _mapper.Map<List<VehicleTypeDto>>(result)
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error listando Tipo de vehiculos");
            return new()
            {
                Result = Result.FailWithException(e),
                Data = new()
            };
        }
    }
}

#endregion