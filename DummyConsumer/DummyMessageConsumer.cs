using DummyRmq.Shared.Queues.Dummy;
using DummyRmq.Shared.Queues.Dummy.Result;
using MassTransit;

namespace DummyConsumer;

internal class DummyMessageConsumer : IConsumer<DummyMessage>
{
    public async Task Consume(ConsumeContext<DummyMessage> context)
    {
        var model = context.Message;

        await context.Publish(new DummyEvent(model.Guid, model.Fail));
    }
}
