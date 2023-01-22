namespace DummyRmq.Shared.Configurations;

public sealed class SenderConfiguration
{
    public const string SectionName = nameof(SenderConfiguration);

    public int MessagesCount { get; set; }
}
