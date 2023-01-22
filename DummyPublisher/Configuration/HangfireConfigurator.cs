using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DummyPublisher.Configuration;

public static class HangfireConfigurator
{
    public static IServiceCollection ConfigureHangfire(this IServiceCollection services, IConfiguration configuration) =>
        services.AddHangfire(config =>
            {
                config
                    .UsePostgreSqlStorage(configuration.GetConnectionString("HangfireConnection"))
                    .UseColouredConsoleLogProvider();
            })
            .AddHangfireServer();
}
