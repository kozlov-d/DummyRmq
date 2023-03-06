using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DummyPublisher.Infrastructure;

public class DummyContextFactory : IDesignTimeDbContextFactory<DummyContext>
{
    public DummyContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DummyContext>();
        optionsBuilder.UseNpgsql("User ID=postgres;Password=qwerty;Host=localhost;Port=5432;Database=dummy;");

        return new DummyContext(optionsBuilder.Options);
    }
}
