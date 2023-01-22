using DummyPublisher;
using DummyPublisher.Configuration;
using DummyRmq.Shared.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var hostBuilder = Host.CreateDefaultBuilder(args);

var host = hostBuilder.ConfigureServices((context, services) =>
        services
            .Configure<SenderConfiguration>(context.Configuration.GetSection(SenderConfiguration.SectionName))
            .AddScoped<IMessageSender, MessageSender>()
            .ConfigureHangfire(context.Configuration)
            .ConfigureMassTransit(context.Configuration.GetRequiredSection(MassTransitConfiguration.SectionName).Get<MassTransitConfiguration>()!)
            .AddHostedService<JobsCreator>())
    .Build();

host.Run();
