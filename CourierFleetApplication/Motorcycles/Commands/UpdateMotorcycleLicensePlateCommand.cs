using CourierFleetDomain.Interfaces;
using MediatR;

namespace CourierFleetApplication.Motorcycles.Commands;

public class UpdateMotorcycleLicensePlateCommand : IRequest<Unit>
{
    public int MotorcycleId { get; set; }
    public string NewLicensePlate { get; set; }
}

public class UpdateMotorcycleLicensePlateCommandHandler(IMotorcycleRepository motorcycleRepository) : IRequestHandler<UpdateMotorcycleLicensePlateCommand, Unit>
{
    private readonly IMotorcycleRepository _motorcycleRepository = motorcycleRepository;

    public async Task<Unit> Handle(UpdateMotorcycleLicensePlateCommand request, CancellationToken cancellationToken)
    {
        var motorcycle = await _motorcycleRepository.GetByIdAsync(request.MotorcycleId);

        if (motorcycle == null)
        {
            throw new Exception("Motorcycle not found.");
        }

        motorcycle.UpdateLicensePlate(request.NewLicensePlate);
        await _motorcycleRepository.UpdateAsync(motorcycle);

        return Unit.Value;
    }
}