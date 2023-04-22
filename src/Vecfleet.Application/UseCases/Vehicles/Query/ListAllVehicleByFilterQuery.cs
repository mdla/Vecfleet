using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Vecfleet.Application.Dtos.Common;
using Vecfleet.Application.Interfaces;
using Vecfleet.Application.Interfaces.Common;
using Vecfleet.Application.Interfaces.Response;
using Vecfleet.Application.UseCases.VehicleTypes.Query;

namespace Vecfleet.Application.UseCases.Vehicles.Query;

#region Contracts

public class ListAllVehicleByFilterRequest
{
    public int? BrandId { get; set; }
    public int? ModelId { get; set; }
}

public class ListAllVehicleByFilterResponse : IListDtoResponse<VehicleDto>
{
    public Result Result { get; set; }
    public List<VehicleDto> Data { get; set; }
}

#endregion

#region command

public class ListAllVehicleByFilterQuery : IRequest<ListAllVehicleByFilterResponse>
{
    public readonly ListAllVehicleByFilterRequest _request;

    public ListAllVehicleByFilterQuery(ListAllVehicleByFilterRequest request)
    {
        _request = request;
    }
}

public class
    ListAllVehicleByFilterQueryHandler : IRequestHandler<ListAllVehicleByFilterQuery, ListAllVehicleByFilterResponse>
{
    private readonly ILogger<ListAllVehicleByFilterQueryHandler> _logger;
    private readonly ISimpleCrudDbContext _dbContext;
    private readonly IMapper _mapper;

    public ListAllVehicleByFilterQueryHandler(ILogger<ListAllVehicleByFilterQueryHandler> logger,
        ISimpleCrudDbContext dbContext,
        IMapper mapper)
    {
        _logger = logger;
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ListAllVehicleByFilterResponse> Handle(ListAllVehicleByFilterQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await _dbContext.Vehicles
                .Include(x => x.Brand)
                .Include(x => x.Model)
                .Where(x => (request._request.BrandId == null || x.BrandId == request._request.BrandId)
                            && (request._request.ModelId == null || x.ModelId == request._request.ModelId))
                .OrderBy(x => x.Patent)
                .ToListAsync(cancellationToken);

            return new()
            {
                Result = Result.Successful(),
                Data = _mapper.Map<List<VehicleDto>>(result)
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error listando vehiculos");
            return new()
            {
                Result = Result.FailWithException(e),
                Data = new()
            };
        }
    }
}

#endregion