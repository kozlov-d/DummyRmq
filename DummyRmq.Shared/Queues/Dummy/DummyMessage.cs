namespace DummyRmq.Shared.Queues.Dummy;

public record DummyMessage(Guid Guid, int BatchIndex, bool Fail);
