using DummyPublisher.Domain.Entities;
using DummyPublisher.Infrastructure;
using DummyRmq.Shared.Configurations;
using DummyRmq.Shared.Queues.Dummy;
using MassTransit;
using Microsoft.Extensions.Options;

namespace DummyPublisher;

internal class MessageSender : IMessageSender
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly DummyContext _context;
    private readonly SenderConfiguration _senderConfiguration;

    public MessageSender(IOptions<SenderConfiguration> senderConfiguration, IPublishEndpoint publishEndpoint, DummyContext context)
    {
        _publishEndpoint = publishEndpoint;
        _context = context;
        _senderConfiguration = senderConfiguration.Value;
    }

    public async Task SendMessages(CancellationToken ct = default)
    {
        var messages = GenerateMessages().ToList();

        await SaveMessages(messages, ct);

        foreach (var message in messages)
        {
            await _publishEndpoint.Publish(message, ct);
        }
    }

    private IEnumerable<DummyMessage> GenerateMessages()
    {
        foreach (var batchIndex in Enumerable.Range(0, _senderConfiguration.MessagesCount))
        {
            var fail = (double)Random.Shared.Next(0, 100) / 100 < 0.3;
            yield return new DummyMessage(NewId.NextGuid(), batchIndex, fail, 0);
        }
    }

    private async Task SaveMessages(IEnumerable<DummyMessage> messagesToSave, CancellationToken ct)
    {
        _context
            .DummyEntities
            .AddRange(messagesToSave
                .Select(m => new DummyEntity
                {
                    Id = m.Guid,
                    Status = null
                }));

        await _context.SaveChangesAsync(ct);
    }
}
