using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vecfleet.Domain.Entities;

namespace Vecfleet.Infrastructure.Persistence.Configuration;

public class ModelConfiguration : IEntityTypeConfiguration<Model>
{
    public void Configure(EntityTypeBuilder<Model> builder)
    {
        builder.ToTable("Models");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("Id");


        builder.Property(x => x.Description)
            .IsRequired(true)
            .HasColumnName("Description").HasMaxLength(64);

        builder.HasOne(x => x.Brand);

    }
}