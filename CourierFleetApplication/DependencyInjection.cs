using CourierFleetApplication.Mappings;
using Microsoft.Extensions.DependencyInjection;

namespace CourierFleetApplication
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
              cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

            services.AddAutoMapper(typeof(Profiles).Assembly);

            return services;
        }
    }
}