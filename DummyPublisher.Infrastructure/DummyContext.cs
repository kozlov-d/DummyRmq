using System.Reflection;
using DummyPublisher.Infrastructure.EntityTypeConfigurations;
using DummyRmq.Shared.Queues.Dummy;
using Microsoft.EntityFrameworkCore;

namespace DummyPublisher.Infrastructure;

public class DummyContext : DbContext
{
    private DbSet<DummyMessage> DummyMessages { get; set; } = null!;

    public DummyContext(DbContextOptions<DummyContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(DummyMessageConfiguration))!);
    }
}
