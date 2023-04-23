using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Vecfleet.Application.Dtos.Common;
using Vecfleet.Application.Interfaces;
using Vecfleet.Application.Interfaces.Common;
using Vecfleet.Application.Interfaces.Request;
using Vecfleet.Application.Interfaces.Response;
using Vecfleet.Application.UseCases.Brands.Query;

namespace Vecfleet.Application.UseCases.Models.Query;

#region Contracts

public class FilterModelByBrandQueryRequest : IIdRequest<int>
{
    public int Id { get; set; }
}

public class FilterModelByBrandQueryResponse : IListDtoResponse<ModelDto>
{
    public Result Result { get; set; }
    public List<ModelDto> Data { get; set; }
}

#endregion

#region command

public class FilterModelByBrandQuery : IRequest<FilterModelByBrandQueryResponse>
{
    public readonly FilterModelByBrandQueryRequest _request;

    public FilterModelByBrandQuery(FilterModelByBrandQueryRequest request)
    {
        _request = request;
    }
}

public class FilterModelByBrandQueryHandler : IRequestHandler<FilterModelByBrandQuery, FilterModelByBrandQueryResponse>
{
    private readonly ILogger<FilterModelByBrandQueryHandler> _logger;
    private readonly ISimpleCrudDbContext _dbContext;
    private readonly IMapper _mapper;

    public FilterModelByBrandQueryHandler(ILogger<FilterModelByBrandQueryHandler> logger, ISimpleCrudDbContext dbContext,
        IMapper mapper)
    {
        _logger = logger;
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<FilterModelByBrandQueryResponse> Handle(FilterModelByBrandQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _dbContext.Models
                .Where(x=> request._request.Id == x.BrandId)
                .OrderBy(x => x.Description)
                .ToListAsync(cancellationToken);

            return new()
            {
                Result = Result.Successful(),
                Data = _mapper.Map<List<ModelDto>>(result)
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error listando Marcas");
            return new()
            {
                Result = Result.FailWithException(e),
                Data = new()
            };
        }
    }
}

#endregion