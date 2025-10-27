using CourierFleetDomain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourierFleetInfrastructure.Data.ModelConfiguration;

public class RentalConfiguration : IEntityTypeConfiguration<Rental>
{
    public void Configure(EntityTypeBuilder<Rental> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.CourierId)
            .IsRequired();

        builder.Property(r => r.MotorcycleId)
            .IsRequired();

        builder.Property(r => r.StartDate)
            .IsRequired();

        builder.Property(r => r.EndDate);

        builder.Property(r => r.ExpectedEndDate)
            .IsRequired();

        builder.Property(r => r.Plan)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.HasOne(r => r.Courier)
            .WithMany()
            .HasForeignKey(r => r.CourierId);

        builder.HasOne(r => r.Motorcycle)
            .WithMany()
            .HasForeignKey(r => r.MotorcycleId);
    }
}