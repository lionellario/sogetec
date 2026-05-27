namespace Sogetec.Chassis.Exceptions;

public static class ExceptionExtensions
{
    extension(IServiceCollection services)
    {
        /// <summary>
        ///     Registers the <see cref="InvalidOperationExceptionHandler" /> in the ASP.NET Core exception handling pipeline.
        /// </summary>
        public IServiceCollection AddInvalidOperationExceptionHandler()
        {
            services.AddExceptionHandler<InvalidOperationExceptionHandler>();
            return services;
        }

        /// <summary>
        ///     Registers the global exception handler implementation used to map unhandled exceptions to standardized problem
        ///     responses.
        /// </summary>
        public IServiceCollection AddGlobalExceptionHandler()
        {
            services.AddExceptionHandler<GlobalExceptionHandler>();
            return services;
        }

        /// <summary>
        ///     Registers the <see cref="NotFoundExceptionHandler" /> in the ASP.NET Core exception handling pipeline.
        /// </summary>
        public IServiceCollection AddNotFoundExceptionHandler()
        {
            services.AddExceptionHandler<NotFoundExceptionHandler>();
            return services;
        }

        /// <summary>
        ///     Registers the validation exception handler so FluentValidation failures are returned as standardized validation
        ///     problem responses.
        /// </summary>
        public IServiceCollection AddValidationExceptionHandler()
        {
            services.AddExceptionHandler<ValidationExceptionHandler>();
            return services;
        }
    }
}
