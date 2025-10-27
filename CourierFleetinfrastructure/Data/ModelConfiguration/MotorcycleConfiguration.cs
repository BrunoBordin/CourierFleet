using CourierFleetDomain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourierFleetInfrastructure.Data.ModelConfiguration;

public class MotorcycleConfiguration : IEntityTypeConfiguration<Motorcycle>
{
    public void Configure(EntityTypeBuilder<Motorcycle> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Identifier)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(m => m.Year)
            .IsRequired();

        builder.Property(m => m.Model)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(m => m.LicensePlate)
            .IsRequired()
            .HasMaxLength(7);

        builder.HasIndex(m => m.LicensePlate)
            .IsUnique();
    }
}