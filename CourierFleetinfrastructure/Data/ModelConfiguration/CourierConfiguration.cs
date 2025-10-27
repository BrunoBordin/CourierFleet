using CourierFleetDomain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourierFleetInfrastructure.Data.ModelConfiguration;

public class CourierConfiguration : IEntityTypeConfiguration<Courier>
{
    public void Configure(EntityTypeBuilder<Courier> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Cnpj)
            .IsRequired()
            .HasMaxLength(14);

        builder.HasIndex(c => c.Cnpj)
            .IsUnique();

        builder.Property(c => c.BirthDate)
            .IsRequired();

        builder.Property(c => c.CnhNumber)
            .IsRequired()
            .HasMaxLength(11);

        builder.HasIndex(c => c.CnhNumber)
            .IsUnique();

        builder.Property(c => c.CnhType)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(2);

        builder.Property(c => c.CnhImage)
            .HasMaxLength(400);
    }
}
