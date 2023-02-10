using DummyRmq.Shared.Queues.Dummy;
using DummyRmq.Shared.Queues.Dummy.Result;
using MassTransit;

namespace DummyConsumer;

internal sealed class DummyMessageConsumer : IConsumer<DummyMessage>
{
    public async Task Consume(ConsumeContext<DummyMessage> context)
    {
        Thread.Sleep(Random.Shared.Next(500, 1500));

        var model = context.Message;

        await context.Publish(new DummyEvent(model.Guid, model.Fail));
    }
}

internal class DummyMessageConsumerDefinition : ConsumerDefinition<DummyMessageConsumer>
{
    public void Configure(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<DummyMessageConsumer> consumerConfigurator)
    {
        endpointConfigurator.UseMessageRetry(r => r.Intervals(100, 100, 100));
        endpointConfigurator.PrefetchCount = 1;
    }
}
