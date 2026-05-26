using Sogetec.Chassis.CQRS.Command;
using Sogetec.Chassis.CQRS.Pipelines;
using Sogetec.Chassis.CQRS.Query;

namespace Sogetec.Chassis.CQRS;

public static class Extensions
{
    extension(IServiceCollection services)
    {
        /// <summary>
        ///     Adds the command handler metrics collector to the service collection.
        /// </summary>
        /// <returns>
        ///     The current service collection.
        /// </returns>
        public IServiceCollection AddCommandHandlerMetrics()
        {
            services.AddSingleton<CommandHandlerMetrics>();
            return services;
        }

        /// <summary>
        ///     Adds the query handler metrics collector to the service collection.
        /// </summary>
        /// <returns>
        ///     The current service collection.
        /// </returns>
        public IServiceCollection AddQueryHandlerMetrics()
        {
            services.AddSingleton<QueryHandlerMetrics>();
            return services;
        }

        /// <summary>
        ///     Registers the activity pipeline behavior.
        /// </summary>
        /// <returns>
        ///     The current service collection.
        /// </returns>
        public IServiceCollection ApplyActivityBehavior()
        {
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ActivityBehavior<,>));
            return services;
        }

        /// <summary>
        ///     Registers the logging pipeline behavior.
        /// </summary>
        /// <returns>
        ///     The current service collection.
        /// </returns>
        public IServiceCollection ApplyLoggingBehavior()
        {
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            return services;
        }

        /// <summary>
        ///     Registers the validation pipeline behavior.
        /// </summary>
        /// <returns>
        ///     The current service collection.
        /// </returns>
        public IServiceCollection ApplyValidationBehavior()
        {
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services;
        }
    }
}
