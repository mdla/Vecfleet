using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Vecfleet.Application.Interfaces;
using Vecfleet.Application.UseCases.Brands.Dto;

namespace Vecfleet.Application.UseCases.Brands.Query;
//
// public class ListBrandQuery: IRequest<List<BrandDto>>
// {
// }
//
// public class ListBrandQueryHandler: IRequestHandler<ListBrandQuery,List<BrandDto>>
// {
//     private readonly ILogger<ListBrandQueryHandler> _logger;
//     private readonly ISimpleCrudDbContext _dbContext;
//  private readonly IMapper _mapper;
//
//     public ListBrandQueryHandler(ILogger<ListBrandQueryHandler> logger, ISimpleCrudDbContext dbContext, IMapper mapper)
//     {
//         _logger = logger;
//         _dbContext = dbContext;
//
//         _mapper = mapper;
//     }
//
//     public async Task<List<BrandDto>> Handle(ListBrandQuery request, CancellationToken cancellationToken)
//     {
//         try
//         {
//             var result = await _dbContext.Brands.OrderBy(x => x.Name).ToListAsync(cancellationToken);
//
//             return _mapper.Map<List<BrandDto>>(result);
//
//         }
//         catch (Exception e)
//         {
//             _logger.LogError(e,"Error listando Marcas");
//             return 
//         }
//     }
// }