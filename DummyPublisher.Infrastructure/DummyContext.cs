using System.Reflection;
using DummyPublisher.Domain.Entities;
using DummyPublisher.Infrastructure.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace DummyPublisher.Infrastructure;

public class DummyContext : DbContext
{
    public DbSet<DummyEntity> DummyEntities { get; set; } = null!;

    public DummyContext(DbContextOptions<DummyContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(DummyEntityConfiguration))!);
    }
}
