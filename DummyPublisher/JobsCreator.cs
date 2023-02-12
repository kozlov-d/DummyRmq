using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DummyPublisher;

public class JobsCreator : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public JobsCreator(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        await using var scope = _scopeFactory.CreateAsyncScope();
        var messageSender = scope.ServiceProvider.GetRequiredService<IMessageSender>();
        RecurringJob.AddOrUpdate("DummyJob", () => messageSender.SendMessages(ct), "*/30 * * * * *");
    }
}
