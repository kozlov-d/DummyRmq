namespace DummyPublisher;

public interface IMessageSender
{
    public Task SendMessages(CancellationToken ct = default);
}
