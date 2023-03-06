using DummyPublisher.Domain.Enums;

namespace DummyPublisher.Domain.Entities;

public sealed class DummyEntity
{
    public Guid Id { get; set; }
    public Status? Status { get; set; }
}
