using CourierFleetApplication.Motorcycles.Commands;
using CourierFleetDomain.Interfaces;
using CourierFleetInfrastructure.Consumer;
using CourierFleetInfrastructure.Data;
using CourierFleetInfrastructure.Data.Repositories;
using CourierFleetInfrastructure.Options;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CourierFleetInfrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<CourierFleetDbContext>(opt =>
            opt.UseNpgsql(connectionString));

        services.AddOptions<RabbitMQOptions>().Bind(configuration.GetSection("RabbitMQ")).ValidateDataAnnotations().ValidateOnStart();

        services.AddMassTransit(x =>
        {
            x.AddConsumer<MotorcycleCreatedConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                var rabbitOptions = context.GetRequiredService<IOptions<RabbitMQOptions>>().Value;

                cfg.Host(rabbitOptions.HostName, h =>
                {
                    h.Username(rabbitOptions.UserName);
                    h.Password(rabbitOptions.Password);
                });

                cfg.ReceiveEndpoint(rabbitOptions.Queue, e =>
                {
                    e.ConfigureConsumer<MotorcycleCreatedConsumer>(context);
                });
            });
        });

        services.AddScoped<IMotorcycleRepository, MotorcycleRepository>();
        services.AddScoped<IRentalRepository, RentalRepository>();
        services.AddScoped<ICourierRepository, CourierRepository>();

        return services;
    }
}