﻿using DummyRmq.Shared.Configurations;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace DummyConsumer;

public static class MassTransitConfigurator
{
    public static IServiceCollection ConfigureMassTransit(this IServiceCollection services, MassTransitConfiguration mtConfiguration) =>
        services.AddMassTransit(bus =>
        {
            bus.SetKebabCaseEndpointNameFormatter();
            bus.AddConsumer<DummyMessageConsumer>();

            bus.UsingRabbitMq((ctx, rmq) =>
            {
                rmq.Host(mtConfiguration.Host, 5672, mtConfiguration.VirtualHost, h =>
                {
                    h.Username(mtConfiguration.Username);
                    h.Password(mtConfiguration.Password);
                });
                rmq.ConfigureEndpoints(ctx, new KebabCaseEndpointNameFormatter("", false));
            });
        });
}
