using CourierFleetDomain.Entities;

namespace CourierFleetDomain.Interfaces;

public interface IMotorcycleRepository
{
    Task AddAsync(Motorcycle motorcycle);

    Task<Motorcycle> GetByIdAsync(int id);

    Task UpdateAsync(Motorcycle motorcycle);

    Task RemoveAsync(int id);

    Task<Motorcycle> GetByLicensePlateAsync(string licensePlate);

    Task<IEnumerable<Motorcycle>> GetAllAsync();
}