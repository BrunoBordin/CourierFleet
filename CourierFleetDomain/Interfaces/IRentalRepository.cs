using CourierFleetDomain.Entities;

namespace CourierFleetDomain.Interfaces;

public interface IRentalRepository
{
    Task AddAsync(Rental rental);

    Task<Rental> GetByIdAsync(int id);

    Task<bool> HasRentalsForMotorcycleAsync(int motorcycleId);
}