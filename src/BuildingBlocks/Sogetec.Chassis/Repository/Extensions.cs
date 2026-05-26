namespace Sogetec.Chassis.Repository;

public static class Extensions
{
    extension(IServiceCollection services)
    {
        /// <summary>
        ///     Registers all Pre Authentication repository implementations from the assembly that contains the specified type. <br/>
        ///     These repostories are meant to lookup data before authentication. <br/>
        ///     The repository implementations are registered as <see cref="IPreAuthenticationRepository" />.
        /// </summary>
        /// <param name="type">A type used to locate the target assembly for repository scanning.</param>
        public void AddPreAuthenticationRepositories(Type type)
        {
            services.Scan(scan =>
                scan.FromAssembliesOf(type)
                    .AddClasses(classes => classes.AssignableTo<IPreAuthenticationRepository>(), false)
                    .AsImplementedInterfaces()
                    .WithSingletonLifetime()
            );
        }
    }
}
