namespace DummyRmq.Shared.Configurations;

public class MassTransitConfiguration
{
    public const string SectionName = nameof(MassTransitConfiguration);

    public string Host { get; set; }
    public string VirtualHost { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}
