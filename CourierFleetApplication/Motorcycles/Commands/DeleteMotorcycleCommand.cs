using CourierFleetDomain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CourierFleetApplication.Motorcycles.Commands;

public class DeleteMotorcycleCommand : IRequest<Unit>
{
    public int MotorcycleId { get; set; }
}

public class DeleteMotorcycleCommandHandler(IMotorcycleRepository motorcycleRepository, IRentalRepository rentalRepository, ILogger<DeleteMotorcycleCommandHandler> logger) : IRequestHandler<DeleteMotorcycleCommand, Unit>
{
    private readonly IMotorcycleRepository _motorcycleRepository = motorcycleRepository;
    private readonly IRentalRepository _rentalRepository = rentalRepository;
    private readonly ILogger<DeleteMotorcycleCommandHandler> _logger = logger;

    public async Task<Unit> Handle(DeleteMotorcycleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var hasRentals = await _rentalRepository.HasRentalsForMotorcycleAsync(request.MotorcycleId);

            if (hasRentals)
            {
                throw new Exception("Motorcycle cannot be removed as it has associated rental records.");
            }

            await _motorcycleRepository.RemoveAsync(request.MotorcycleId);
            return Unit.Value;
        }
        catch (Exception ex)
        {
            string message = "Error deleting motorcycle";
            _logger.LogError(ex, "Error deleting motorcycle.");
            throw new Exception(message); ;
        }
    }
}