using DummyPublisher.Domain.Enums;
using DummyPublisher.Infrastructure;
using DummyRmq.Shared.Queues.Dummy.Result;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace DummyPublisher;

internal sealed class DummyEventConsumer : IConsumer<DummyEvent>
{
    private readonly DummyContext _dbContext;

    public DummyEventConsumer(DummyContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Consume(ConsumeContext<DummyEvent> consumeContext)
    {
        var message = consumeContext.Message;
        var entityToUpdate = await _dbContext.DummyEntities.FirstAsync(e => e.Id == message.Guid);

        entityToUpdate.Status = message.IsFailed ? Status.Failed : Status.Delivered;

        await _dbContext.SaveChangesAsync();
    }
}
