using DummyRmq.Shared.Queues.Dummy;
using DummyRmq.Shared.Queues.Dummy.Result;
using MassTransit;

namespace DummyConsumer;

internal sealed class DummyMessageConsumer : IConsumer<DummyMessage>
{
    public async Task Consume(ConsumeContext<DummyMessage> context)
    {
        Thread.Sleep(Random.Shared.Next(300, 600));

        var message = context.Message;
        if (message.FailsCount > 2 && message.Fail)
        {
            await context.Publish(new DummyEvent(message.Guid, message.Fail));
        }
        else
        {
            await context.Publish(message with { FailsCount = message.FailsCount + 1});
            return;
        }

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
        endpointConfigurator.UseMessageRetry(r => r.Intervals(50, 50, 50));
        endpointConfigurator.DiscardFaultedMessages();
    }
}
