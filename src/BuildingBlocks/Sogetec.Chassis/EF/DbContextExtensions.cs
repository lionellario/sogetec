using Microsoft.EntityFrameworkCore.Diagnostics;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

namespace Sogetec.Chassis.EF;

public static class DbContextExtensions
{
    extension(IHostApplicationBuilder builder)
    {
        public void AddPostgresDbContext<TDbContext>(
            string name,
            Action<NpgsqlDbContextOptionsBuilder>? npgsqlOptionsAction = null) where TDbContext : DbContext
        {
            builder.Services.AddDbContext<TDbContext>((sp, options) =>
            {
                var logger = sp.GetRequiredService<ILogger<TDbContext>>();
                options.UseNpgsql(builder.Configuration.GetConnectionString(name), npgsqlOptionsAction);

                // Resolve and apply all registered EF Core interceptors.
                var interceptors = sp.GetServices<IInterceptor>().ToArray();

                if (interceptors.Length != 0)
                {
                    options.AddInterceptors(interceptors);
                }
            });
        }
    }
}
