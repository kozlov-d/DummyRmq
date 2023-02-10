namespace DummyRmq.Shared.Queues.Dummy.Result;

public sealed record DummyEvent(Guid Guid, bool IsFailed);
