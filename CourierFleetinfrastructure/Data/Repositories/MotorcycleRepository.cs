using CourierFleetDomain.Entities;
using CourierFleetDomain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourierFleetInfrastructure.Data.Repositories;

public class MotorcycleRepository(CourierFleetDbContext context) : IMotorcycleRepository
{
    private readonly CourierFleetDbContext _context = context;

    public async Task AddAsync(Motorcycle motorcycle)
    {
        await _context.Motorcycles.AddAsync(motorcycle);
        await _context.SaveChangesAsync();
    }

    public async Task<Motorcycle> GetByIdAsync(int id)
    {
        return await _context.Motorcycles.FindAsync(id);
    }

    public async Task UpdateAsync(Motorcycle motorcycle)
    {
        _context.Motorcycles.Update(motorcycle);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(int id)
    {
        var motorcycle = await GetByIdAsync(id);
        if (motorcycle != null)
        {
            _context.Motorcycles.Remove(motorcycle);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Motorcycle> GetByLicensePlateAsync(string licensePlate)
    {
        return await _context.Motorcycles.FirstOrDefaultAsync(m => m.LicensePlate == licensePlate);
    }

    public async Task<IEnumerable<Motorcycle>> GetAllAsync()
    {
        return await _context.Motorcycles.ToListAsync();
    }
}