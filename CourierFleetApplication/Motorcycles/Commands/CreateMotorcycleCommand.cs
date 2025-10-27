using CourierFleetApplication.Motorcycles.Event;
using CourierFleetDomain.Entities;
using CourierFleetDomain.Interfaces;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CourierFleetApplication.Motorcycles.Commands;

public class CreateMotorcycleCommand : IRequest<int>
{
    public string Identifier { get; set; }

    public int Year { get; set; }

    public string Model { get; set; }

    public string LicensePlate { get; set; }
}

public class CreateMotorcycleCommandHandler(IMotorcycleRepository motorcycleRepository, IBus bus, ILogger<CreateMotorcycleCommandHandler> logger) : IRequestHandler<CreateMotorcycleCommand, int>
{
    private readonly IMotorcycleRepository _motorcycleRepository = motorcycleRepository;
    private readonly IBus _bus = bus;
    private readonly ILogger<CreateMotorcycleCommandHandler> _logger = logger;

    public async Task<int> Handle(CreateMotorcycleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var existingMotorcycle = await _motorcycleRepository.GetByLicensePlateAsync(request.LicensePlate);

            if (existingMotorcycle != null)
            {
                throw new Exception("License plate already exists.");
            }

            var motorcycle = new Motorcycle(request.Identifier, request.Year, request.Model, request.LicensePlate);
            await _motorcycleRepository.AddAsync(motorcycle);

            if (motorcycle.Year == 2024)
            {
                var createdEvent = new MotorcycleCreatedEvent
                {
                    Id = motorcycle.Id,
                    Identifier = motorcycle.Identifier,
                    Year = motorcycle.Year,
                    Model = motorcycle.Model,
                    LicensePlate = motorcycle.LicensePlate
                };

                await _bus.Publish(createdEvent, cancellationToken);
            }

            return motorcycle.Id;
        }
        catch (Exception ex)
        {
            string message = "Error saving motorcycle";
            _logger.LogError(ex, message);
            throw new Exception(message); ;
        }
    }
}