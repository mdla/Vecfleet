using AutoMapper;
using Vecfleet.Application.Dtos.Common;
using Vecfleet.Domain.Entities;

namespace Vecfleet.Application.Maps;

public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        // Mapeos
        CreateMap<Brand, BrandDto>().ReverseMap();
        CreateMap<VehicleType, VehicleTypeDto>().ReverseMap();
        CreateMap<Model, ModelDto>().ReverseMap();
        CreateMap<Vehicle, VehicleDto>().ReverseMap();
    }
}