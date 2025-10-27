using CourierFleetApplication.Motorcycles.Event;
using CourierFleetDomain.Entities;
using CourierFleetInfrastructure.Data;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace CourierFleetInfrastructure.Consumer;

public class MotorcycleCreatedConsumer(CourierFleetDbContext context, ILogger<MotorcycleCreatedConsumer> logger) : IConsumer<MotorcycleCreatedEvent>
{
    private readonly CourierFleetDbContext _context = context;
    private readonly ILogger<MotorcycleCreatedConsumer> _logger = logger;

    public async Task Consume(ConsumeContext<MotorcycleCreatedEvent> context)
    {
        try
        {
            var notification = new MotorcycleNotification
            {
                MotorcycleId = context.Message.Id,
                Identifier = context.Message.Identifier,
                Year = context.Message.Year,
                Model = context.Message.Model,
                LicensePlate = context.Message.LicensePlate,
                NotificationDate = DateTime.UtcNow
            };

            await _context.MotorcycleNotifications.AddAsync(notification);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to save notification.");
        }
    }
}