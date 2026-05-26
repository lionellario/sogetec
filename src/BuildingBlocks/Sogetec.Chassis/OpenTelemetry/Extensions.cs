namespace Sogetec.Chassis.OpenTelemetry;

public static class Extensions
{
    extension(IServiceCollection services)
    {
        /// <summary>
        ///     Registers the activity scope service in the dependency injection container.
        /// </summary>
        /// <returns>The updated <see cref="IServiceCollection" /> instance.</returns>
        public IServiceCollection AddActivityScope()
        {
            services.AddSingleton<IActivityScope, ActivityScope.ActivityScope>();
            return services;
        }
    }
}
