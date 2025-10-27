using CourierFleetDomain.Entities;

namespace CourierFleetDomain.Interfaces;

public interface ICourierRepository
{
    Task AddAsync(Courier courier);

    Task<Courier> GetByIdAsync(int id);

    Task UpdateAsync(Courier courier);

    Task<Courier> GetByCnpjAsync(string cnpj);

    Task<Courier> GetByCnhNumberAsync(string cnhNumber);
}