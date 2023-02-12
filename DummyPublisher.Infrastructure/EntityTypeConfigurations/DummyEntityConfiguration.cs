using DummyPublisher.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DummyPublisher.Infrastructure.EntityTypeConfigurations;

internal sealed class DummyEntityConfiguration : IEntityTypeConfiguration<DummyEntity>
{
    public void Configure(EntityTypeBuilder<DummyEntity> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Status).HasConversion<string>();
    }
}
