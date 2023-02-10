using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DummyPublisher.Infrastructure;

public static class InfrastructureConfigurator
{
    public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services, string connectionString)
    {
        services
            .AddDbContext<DummyContext>(opt => opt.UseNpgsql(connectionString));

        return services;
    }
}
