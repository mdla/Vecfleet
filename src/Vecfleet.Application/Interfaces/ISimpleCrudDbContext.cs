using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Vecfleet.Domain.Entities;

namespace Vecfleet.Application.Interfaces;

public interface ISimpleCrudDbContext
{
    DbSet<Brand> Brands { get; set; }
    DbSet<VehicleType> VehicleTypes { get; set; }
    DbSet<Model> Models { get; set; }
    
    DbSet<Vehicle> Vehicles { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    DatabaseFacade Database { get; }
}