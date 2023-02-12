using DummyPublisher.Domain.Entities;
using DummyPublisher.Domain.Enums;
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
        var messages = GenerateMessages();

        SaveMessages(messages);

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
            yield return new DummyMessage(NewId.NextGuid(), batchIndex, fail);
        }
    }

    private void SaveMessages(IEnumerable<DummyMessage> messagesToSave)
    {
        _context
            .DummyEntities
            .AddRange(messagesToSave
                .Select(m => new DummyEntity
                {
                    Id = m.Guid,
                    Status = Status.Failed
                }));

        _context.SaveChanges();
    }
}
