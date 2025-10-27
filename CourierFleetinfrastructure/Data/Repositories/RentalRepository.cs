using CourierFleetDomain.Entities;
using CourierFleetDomain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourierFleetInfrastructure.Data.Repositories;

public class RentalRepository(CourierFleetDbContext context) : IRentalRepository
{
    private readonly CourierFleetDbContext _context = context;

    public async Task AddAsync(Rental rental)
    {
        await _context.Rentals.AddAsync(rental);
        await _context.SaveChangesAsync();
    }

    public async Task<Rental> GetByIdAsync(int id)
    {
        return await _context.Rentals.FindAsync(id);
    }

    public async Task<bool> HasRentalsForMotorcycleAsync(int motorcycleId)
    {
        return await _context.Rentals.AnyAsync(r => r.MotorcycleId == motorcycleId);
    }
}