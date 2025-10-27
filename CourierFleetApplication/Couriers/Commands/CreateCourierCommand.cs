using CourierFleetDomain.Entities;
using CourierFleetDomain.Enuns;
using CourierFleetDomain.Interfaces;
using MediatR;

namespace CourierFleetApplication.Couriers.Commands;

public class CreateCourierCommand : IRequest<int>
{
    public string Identifier { get; set; }
    public string Name { get; set; }
    public string Cnpj { get; set; }
    public DateTime BirthDate { get; set; }
    public string CnhNumber { get; set; }
    public ECnhType CnhType { get; set; }
}

public class CreateCourierCommandHandler(ICourierRepository courierRepository) : IRequestHandler<CreateCourierCommand, int>
{
    private readonly ICourierRepository _courierRepository = courierRepository;

    public async Task<int> Handle(CreateCourierCommand request, CancellationToken cancellationToken)
    {
        var courier = new Courier(request.Identifier, request.Name, request.Cnpj, request.BirthDate, request.CnhNumber, request.CnhType);
        await _courierRepository.AddAsync(courier);
        return courier.Id;
    }
}
