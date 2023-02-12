using DummyConsumer;
using DummyRmq.Shared.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

var hostBuilder = Host.CreateDefaultBuilder(args);

var host = hostBuilder.ConfigureServices((context, services) =>
    services
        .ConfigureMassTransit(context.Configuration.GetRequiredSection(MassTransitConfiguration.SectionName).Get<MassTransitConfiguration>()!)
).Build();

await host.RunAsync();
