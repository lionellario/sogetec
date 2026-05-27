using Microsoft.EntityFrameworkCore;

namespace Api.Modules.Data.Context;

public class SogetecDbContext(DbContextOptions<SogetecDbContext> Options) : DbContext(Options)
{
    public override int SaveChanges()
    {
        throw new NotImplementedException();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SogetecDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
