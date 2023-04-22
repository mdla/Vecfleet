using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vecfleet.Domain.Entities;

namespace Vecfleet.Infrastructure.Persistence.Configuration;

public class VehicleTypeConfiguration: IEntityTypeConfiguration<VehicleType>
{
    public void Configure(EntityTypeBuilder<VehicleType> builder)
    {
        builder.ToTable("VehicleTypes");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("Id");

        builder.Property(x => x.Description)
            .HasColumnName("Description").HasMaxLength(64);
    }
}