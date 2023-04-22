using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Vecfleet.Application.Interfaces;
using Vecfleet.Domain.Entities;
using Vecfleet.Infrastructure.Persistence.Configuration;

namespace Vecfleet.Infrastructure.Persistence;

public class SimpleCrudDbContext : DbContext, ISimpleCrudDbContext
{
    public SimpleCrudDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Brand> Brands { get; set; }
    public DbSet<VehicleType> VehicleTypes { get; set; }
    public DbSet<Model> Models { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BrandConfiguration());
        modelBuilder.ApplyConfiguration(new ModelConfiguration());
        modelBuilder.ApplyConfiguration(new VehicleConfiguration());
        modelBuilder.ApplyConfiguration(new VehicleTypeConfiguration());

    }
}

