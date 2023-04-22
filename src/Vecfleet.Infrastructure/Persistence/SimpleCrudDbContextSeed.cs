using Microsoft.EntityFrameworkCore;
using Vecfleet.Domain.Entities;

namespace Vecfleet.Infrastructure.Persistence;

public static class SimpleCrudDbContextSeed
{
    public static async Task CreateBrandsAndModels(SimpleCrudDbContext dbContext)
    {
        List<Brand> brands = new List<Brand>()
        {
            new()
            {
                Name = "Ford",
                Models = new List<Model>()
                {
                    new()
                    {
                        Description = "Fiesta"
                    },
                    new()
                    {
                        Description = "Focus"
                    },
                    new()
                    {
                        Description = "Ecosport"
                    }
                }
            },
            new()
            {
                Name = "Chevrolet",
                Models = new List<Model>()
                {
                    new()
                    {
                        Description = "Cruze"
                    },
                    new()
                    {
                        Description = "Onix"
                    }
                }
            },
            new()
            {
                Name = "Fiat",
                Models = new List<Model>()
                {
                    new()
                    {
                        Description = "Argo"
                    },
                    new()
                    {
                        Description = "Cronos"
                    }
                }
            }
        };


        foreach (Brand brand in brands)
        {
            foreach (Model brandModel in brand.Models)
                brandModel.Brand = brand;

            var model = await dbContext.Brands.FirstOrDefaultAsync(x => x.Name.ToUpper() == brand.Name);

            if (model is not null) continue;

            dbContext.Brands.Add(brand);
            foreach (Model brandModel in brand.Models)
                dbContext.Models.Add(brandModel);
        }

        await dbContext.SaveChangesAsync();
    }

    public static async Task CreateVehicleTypes(SimpleCrudDbContext dbContext)
    {
        List<VehicleType> brands = new List<VehicleType>()
        {
            new()
            {
                Description = "Hackback"
            },
            new()
            {
                Description = "Sedan"
            },
            new()
            {
                Description = "Utilitario"
            },
            new()
            {
                Description = "SUV"
            },
        };

        foreach (VehicleType vehicleType in brands)
        {
            var model = await dbContext.VehicleTypes.FirstOrDefaultAsync(x => x.Description.ToUpper() == vehicleType.Description);

            if (model is not null) continue;

            dbContext.VehicleTypes.Add(vehicleType);
        }

        await dbContext.SaveChangesAsync();
    }
}