using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vecfleet.Domain.Entities;

namespace Vecfleet.Infrastructure.Persistence.Configuration;

public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable("Vehicles");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("Id");
        
        builder.HasOne(x => x.VehicleType)
            .WithMany()
            .HasForeignKey(x => x.VehicleTypeId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();
        
        builder.Property(x => x.Wheels)
            .HasColumnName("Wheels")
            .HasMaxLength(16)
            .IsRequired();

        builder.HasOne(x => x.Brand)
            .WithMany().OnDelete(DeleteBehavior.NoAction)
            .HasForeignKey(x => x.BrandId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();
        
        builder.HasOne(x => x.Model)
            .WithMany()
            .HasForeignKey(x => x.ModelId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        builder.Property(x => x.Patent)
            .HasColumnName("Patent")
            .HasMaxLength(16)
            .IsRequired();

        builder.Property(x => x.ChassisNumber)
            .HasColumnName("ChassisNumber")
            .HasMaxLength(64)
            .IsRequired();
        
        builder.Property(x => x.Kilometers)
            .IsRequired(true)
            .HasColumnName("Kilometers");
    }
}