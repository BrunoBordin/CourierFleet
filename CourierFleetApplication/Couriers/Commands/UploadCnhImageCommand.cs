using CourierFleetApplication.Couriers.Commands;
using CourierFleetDomain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CourierFleetApplication.Couriers.Commands
{ 
    public class UploadCnhImageCommand : IRequest<Unit>
    {
        public int CourierId { get; set; }
        public IFormFile File { get; set; }
    }
}

public class UploadCnhImageCommandHandler(ICourierRepository courierRepository) : IRequestHandler<UploadCnhImageCommand, Unit>
{
    private readonly ICourierRepository _courierRepository = courierRepository;

    public async Task<Unit> Handle(UploadCnhImageCommand request, CancellationToken cancellationToken)
    {
        var courier = await _courierRepository.GetByIdAsync(request.CourierId);

        if (courier == null)
        {
            throw new Exception("Courier not found");
        }

        var fileExtension = Path.GetExtension(request.File.FileName).ToLowerInvariant();
        if (fileExtension != ".png" && fileExtension != ".bmp")
        {
            throw new Exception("Invalid file format. Only PNG and BMP");
        }

        var fileName = $"{courier.Id}_{Guid.NewGuid()}{fileExtension}";
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "cnh_images", fileName);

        if (!Directory.Exists(Path.GetDirectoryName(filePath)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
        }

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await request.File.CopyToAsync(stream, cancellationToken);
        }

        courier.UpdateCnhImage(fileName);

        await _courierRepository.UpdateAsync(courier);

        return Unit.Value;
    }
}
