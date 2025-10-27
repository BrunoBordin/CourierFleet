using CourierFleetDomain.Entities;
using CourierFleetDomain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourierFleetInfrastructure.Data.Repositories;

public class CourierRepository(CourierFleetDbContext context) : ICourierRepository
{
    private readonly CourierFleetDbContext _context = context;

    public async Task AddAsync(Courier courier)
    {
        await _context.Couriers.AddAsync(courier);
        await _context.SaveChangesAsync();
    }

    public async Task<Courier> GetByIdAsync(int id)
    {
        return await _context.Couriers.FindAsync(id);
    }

    public async Task UpdateAsync(Courier courier)
    {
        _context.Couriers.Update(courier);
        await _context.SaveChangesAsync();
    }

    public async Task<Courier> GetByCnpjAsync(string cnpj)
    {
        return await _context.Couriers.FirstOrDefaultAsync(c => c.Cnpj == cnpj);
    }

    public async Task<Courier> GetByCnhNumberAsync(string cnhNumber)
    {
        return await _context.Couriers.FirstOrDefaultAsync(c => c.CnhNumber == cnhNumber);
    }
}
