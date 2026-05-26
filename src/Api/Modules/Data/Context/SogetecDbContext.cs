using Microsoft.EntityFrameworkCore;
using Sogetec.Chassis.EF;

namespace Api.Modules.Data.Context;

public class SogetecDbContext(
    DbContextOptions<SogetecDbContext> Options,
    ISessionDataProvider SessionProvider)
  : BaseDbContext(Options, SessionProvider)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SogetecDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
