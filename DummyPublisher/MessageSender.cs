using DummyRmq.Shared.Configurations;
using DummyRmq.Shared.Queues.Dummy;
using MassTransit;
using Microsoft.Extensions.Options;

namespace DummyPublisher;

internal class MessageSender : IMessageSender
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly SenderConfiguration _senderConfiguration;

    public MessageSender(IOptions<SenderConfiguration> senderConfiguration, IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
        _senderConfiguration = senderConfiguration.Value;
    }

    public async Task SendMessages(CancellationToken ct = default)
    {
        foreach (var message in GenerateMessages())
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

    private async Task SaveMessages()
    {

    }
}
