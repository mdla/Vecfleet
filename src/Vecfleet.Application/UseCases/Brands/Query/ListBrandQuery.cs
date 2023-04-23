using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Vecfleet.Application.Dtos.Common;
using Vecfleet.Application.Interfaces;
using Vecfleet.Application.Interfaces.Common;
using Vecfleet.Application.Interfaces.Response;

namespace Vecfleet.Application.UseCases.Brands.Query;

#region Contracts

public class ListAllBrandResponse : IListDtoResponse<BrandDto>
{
    public Result Result { get; set; }
    public List<BrandDto> Data { get; set; }
}

#endregion

#region command

public class ListAllBrandQuery : IRequest<ListAllBrandResponse>
{
}

public class ListAllBrandQueryHandler : IRequestHandler<ListAllBrandQuery, ListAllBrandResponse>
{
    private readonly ILogger<ListAllBrandQueryHandler> _logger;
    private readonly ISimpleCrudDbContext _dbContext;
    private readonly IMapper _mapper;

    public ListAllBrandQueryHandler(ILogger<ListAllBrandQueryHandler> logger, ISimpleCrudDbContext dbContext,
        IMapper mapper)
    {
        _logger = logger;
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ListAllBrandResponse> Handle(ListAllBrandQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _dbContext.Brands
                .Include(x => x.Models)
                .OrderBy(x => x.Name)
                .ToListAsync(cancellationToken);

            return new()
            {
                Result = Result.Successful(),
                Data = _mapper.Map<List<BrandDto>>(result)
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