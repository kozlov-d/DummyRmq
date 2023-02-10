using DummyRmq.Shared.Queues.Dummy.Result;
using MassTransit;

namespace DummyPublisher;

internal sealed class DummyEventConsumer : IConsumer<DummyEvent>
{
    public Task Consume(ConsumeContext<DummyEvent> context)
    {
        return Task.CompletedTask;
    }
}
