using DummyRmq.Shared.Queues.Dummy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DummyPublisher.Infrastructure.EntityTypeConfigurations;

internal sealed class DummyMessageConfiguration : IEntityTypeConfiguration<DummyMessage>
{
    public void Configure(EntityTypeBuilder<DummyMessage> builder)
    {
        builder.HasKey(m => m.Guid);
    }
}
