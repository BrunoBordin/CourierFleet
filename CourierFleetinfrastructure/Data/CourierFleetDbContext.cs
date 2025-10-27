using CourierFleetDomain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CourierFleetInfrastructure.Data;

public class CourierFleetDbContext(DbContextOptions<CourierFleetDbContext> options, IConfiguration configuration) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CourierFleetDbContext).Assembly);
    }

    public DbSet<Courier> Couriers { get; set; }
    public DbSet<Motorcycle> Motorcycles { get; set; }
    public DbSet<Rental> Rentals { get; set; }
    public DbSet<MotorcycleNotification> MotorcycleNotifications { get; set; }
}