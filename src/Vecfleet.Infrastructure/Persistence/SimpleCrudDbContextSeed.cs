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
            },
            new()
            {
                Description = "Mustang"
            },
            new()
            {
                Description = "Ranger"
            },
            new()
            {
                Description = "Explorer"
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
            },
            new()
            {
                Description = "Spark"
            },
            new()
            {
                Description = "Equinox"
            },
            new()
            {
                Description = "Traverse"
            },
            new()
            {
                Description = "Corvette"
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
            },
            new()
            {
                Description = "Mobi"
            },
            new()
            {
                Description = "Uno"
            },
            new()
            {
                Description = "Toro"
            },
            new()
            {
                Description = "500"
            }
        }
    },
    new()
    {
        Name = "Toyota",
        Models = new List<Model>()
        {
            new()
            {
                Description = "Corolla"
            },
            new()
            {
                Description = "Camry"
            },
            new()
            {
                Description = "Yaris"
            },
            new()
            {
                Description = "Rav4"
            },
            new()
            {
                Description = "Highlander"
            },
            new()
            {
                Description = "Tacoma"
            }
        }
    },
    new()
    {
        Name = "Honda",
        Models = new List<Model>()
        {
            new()
            {
                Description = "Civic"
            },
            new()
            {
                Description = "Accord"
            },
            new()
            {
                Description = "Fit"
            },
            new()
            {
                Description = "CR-V"
            },
            new()
            {
                Description = "Pilot"
            },
            new()
            {
                Description = "Odyssey"
            }
        }
    },
    new()
    {
        Name = "Nissan",
        Models = new List<Model>()
        {
            new()
            {
                Description = "Sentra"
            },
            new()
            {
                Description = "Altima"
            },
            new()
            {
                Description = "Versa"
            },
            new()
            {
                Description = "Rogue"
            },
            new()
            {
                Description = "Pathfinder"
            },
            new()
            {
                Description = "Maxima"
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