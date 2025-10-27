using CourierFleetDomain.Entities;
using CourierFleetDomain.Enuns;
using CourierFleetDomain.Interfaces;
using MediatR;

namespace CourierFleetApplication.Rentals.Commands;

public class CreateRentalCommand : IRequest<int>
{
    public int CourierId { get; set; }

    public int MotorcycleId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public DateTime ExpectedEndDate { get; set; }

    public ERentalPlan Plan { get; set; }
}

public class CreateRentalCommandHandler(IRentalRepository rentalRepository, ICourierRepository courierRepository, IMotorcycleRepository motorcycleRepository) : IRequestHandler<CreateRentalCommand, int>
{
    private readonly IRentalRepository _rentalRepository = rentalRepository;
    private readonly ICourierRepository _courierRepository = courierRepository;
    private readonly IMotorcycleRepository _motorcycleRepository = motorcycleRepository;

    public async Task<int> Handle(CreateRentalCommand request, CancellationToken cancellationToken)
    {
        var courier = await _courierRepository.GetByIdAsync(request.CourierId);

        if (courier == null)
        {
            throw new Exception("Courier not found.");
        }

        if (!courier.CanRentMotorcycle())
        {
            throw new Exception("Courier must have CNH type A or A+B to rent a motorcycle.");
        }

        var motorcycle = await _motorcycleRepository.GetByIdAsync(request.MotorcycleId);

        if (motorcycle == null)
        {
            throw new Exception("Motorcycle not found");
        }

        var isMotorcycleNotAvailable = await _rentalRepository.HasRentalsForMotorcycleAsync(request.MotorcycleId);

        if (isMotorcycleNotAvailable)
        {
            throw new Exception("The selected motorcycle is not available for rental ");
        }

        if(request.StartDate == DateTime.UtcNow)
        {
            throw new Exception("The lease must begin on the first day after the creation date");
        }

        var rental = new Rental(request.CourierId, request.MotorcycleId, request.StartDate, request.EndDate, request.ExpectedEndDate, request.Plan);

        await _rentalRepository.AddAsync(rental);

        return rental.Id;
    }
}