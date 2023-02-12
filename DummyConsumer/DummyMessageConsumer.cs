using DummyRmq.Shared.Queues.Dummy;
using DummyRmq.Shared.Queues.Dummy.Result;
using MassTransit;

namespace DummyConsumer;

internal sealed class DummyMessageConsumer : IConsumer<DummyMessage>
{
    public async Task Consume(ConsumeContext<DummyMessage> context)
    {
        Thread.Sleep(Random.Shared.Next(500, 1500));

        var message = context.Message;

        if (message.Fail)
            throw new CommandException("dummy exception");

        await context.Publish(new DummyEvent(message.Guid, message.Fail));
    }
}

internal class DummyMessageConsumerDefinition : ConsumerDefinition<DummyMessageConsumer>
{
    protected override void ConfigureConsumer(
        IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<DummyMessageConsumer> consumerConfigurator)
    {
        endpointConfigurator.UseMessageRetry(r => r.Intervals(100, 100, 100));
        endpointConfigurator.PrefetchCount = 20;
    }
}
