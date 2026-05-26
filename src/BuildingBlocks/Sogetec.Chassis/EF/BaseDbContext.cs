namespace Sogetec.Chassis.EF;

public abstract class BaseDbContext(DbContextOptions options, ISessionDataProvider sp) : DbContext(options)
{
    public readonly SessionData SessionData = sp.GetSessionData();

    public override int SaveChanges()
    {
        throw new NotImplementedException();
    }
}