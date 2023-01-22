using DummyRmq.Shared.Queues.Dummy.Result;
using MassTransit;

namespace DummyPublisher;

public sealed class DummyEventConsumer : IConsumer<DummyEvent>
{
    public Task Consume(ConsumeContext<DummyEvent> context)
    {
        throw new NotImplementedException();
    }
}
