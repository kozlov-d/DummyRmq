namespace DummyRmq.Shared.Queues.Dummy;

public sealed record DummyMessage(Guid Guid, int BatchIndex, bool Fail, uint FailsCount);
