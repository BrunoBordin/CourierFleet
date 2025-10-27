using AutoMapper;
using CourierFleetApplication.Motorcycles.DTOs;
using CourierFleetDomain.Interfaces;
using MediatR;

namespace CourierFleetApplication.Motorcycles.Queries;

public class GetMotorcyclesQuery : IRequest<IEnumerable<MotorcycleDto>>
{
    public string? LicensePlate { get; set; }
}

public class GetMotorcyclesQueryHandler(IMotorcycleRepository motorcycleRepository, IMapper mapper) : IRequestHandler<GetMotorcyclesQuery, IEnumerable<MotorcycleDto>>
{
    private readonly IMotorcycleRepository _motorcycleRepository = motorcycleRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<MotorcycleDto>> Handle(GetMotorcyclesQuery request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(request.LicensePlate))
        {
            var motorcycle = await _motorcycleRepository.GetByLicensePlateAsync(request.LicensePlate);

            return motorcycle != null ? new List<MotorcycleDto> { _mapper.Map<MotorcycleDto>(motorcycle) } : Enumerable.Empty<MotorcycleDto>();
        }

        var motorcycles = await _motorcycleRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<MotorcycleDto>>(motorcycles);
    }
}