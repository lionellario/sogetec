namespace Sogetec.Chassis.Endpoints;

public static class EndpointRouteBuilderExtensions
{
    extension(RouteHandlerBuilder builder)
    {
        /// <summary>
        ///     Configures response metadata for a POST endpoint that returns a created resource.
        /// </summary>
        /// <typeparam name="T">
        ///     The response payload type.
        /// </typeparam>
        /// <param name="hasValidation">
        ///     <see langword="true" /> to include validation problem metadata; otherwise, <see langword="false" />.
        /// </param>
        /// <returns>
        ///     The configured route handler builder.
        /// </returns>
        public RouteHandlerBuilder ProducesPost<T>(bool hasValidation = true)
        {
            builder = builder
                        .Produces<T>(StatusCodes.Status201Created)
                        .ProducesProblem(StatusCodes.Status409Conflict)
                        .ProducesProblem(StatusCodes.Status400BadRequest);

            if (hasValidation)
            {
                builder = builder.ProducesValidationProblem();
            }

            return builder;
        }

        /// <summary>
        ///     Configures response metadata for a POST endpoint that returns a success payload without a location header.
        /// </summary>
        /// <typeparam name="T">
        ///     The response payload type.
        /// </typeparam>
        /// <param name="hasValidation">
        ///     <see langword="true" /> to include validation problem metadata; otherwise, <see langword="false" />.
        /// </param>
        /// <returns>
        ///     The configured route handler builder.
        /// </returns>
        public RouteHandlerBuilder ProducesPostWithoutLocation<T>(bool hasValidation = true)
        {
            builder = builder
                        .Produces<T>()
                        .ProducesProblem(StatusCodes.Status400BadRequest)
                        .ProducesProblem(StatusCodes.Status409Conflict);

            if (hasValidation)
            {
                builder = builder.ProducesValidationProblem();
            }

            return builder;
        }

        /// <summary>
        ///     Configures response metadata for a PUT endpoint.
        /// </summary>
        /// <param name="producesContent">
        ///     <see langword="true" /> to return a payload; otherwise, <see langword="false" />.
        /// </param>
        /// <returns>
        ///     The configured route handler builder.
        /// </returns>
        public RouteHandlerBuilder ProducesPut(bool producesContent = true)
        {
            return builder
                .Produces(producesContent ? StatusCodes.Status200OK : StatusCodes.Status204NoContent)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status409Conflict)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesValidationProblem();
        }

        /// <summary>
        ///     Configures response metadata for a DELETE endpoint.
        /// </summary>
        /// <param name="producesContent">
        ///     <see langword="true" /> to return a payload; otherwise, <see langword="false" />.
        /// </param>
        /// <returns>
        ///     The configured route handler builder.
        /// </returns>
        public RouteHandlerBuilder ProducesDelete(bool producesContent = true)
        {
            return builder
                .Produces(producesContent ? StatusCodes.Status200OK : StatusCodes.Status204NoContent)
                .ProducesProblem(StatusCodes.Status409Conflict)
                .ProducesProblem(StatusCodes.Status404NotFound);
        }

        /// <summary>
        ///     Configures response metadata for a GET endpoint.
        /// </summary>
        /// <typeparam name="T">
        ///     The response payload type.
        /// </typeparam>
        /// <param name="hasValidation">
        ///     <see langword="true" /> to include validation problem metadata; otherwise, <see langword="false" />.
        /// </param>
        /// <param name="hasNotFound">
        ///     <see langword="true" /> to include not found problem metadata; otherwise, <see langword="false" />.
        /// </param>
        /// <returns>
        ///     The configured route handler builder.
        /// </returns>
        public RouteHandlerBuilder ProducesGet<T>(
            bool hasNotFound = true,
            bool hasValidation = false
        )
        {
            builder = builder.Produces<T>();

            if (hasValidation)
            {
                builder = builder.ProducesValidationProblem();
            }

            if (hasNotFound)
            {
                builder = builder.ProducesProblem(StatusCodes.Status404NotFound);
            }

            return builder.ProducesValidationProblem();
        }

        /// <summary>
        ///     Configures response metadata for a PATCH endpoint.
        /// </summary>
        /// <typeparam name="T">
        ///     The response payload type.
        /// </typeparam>
        /// <param name="hasValidation">
        ///     <see langword="true" /> to include validation problem metadata; otherwise, <see langword="false" />.
        /// </param>
        /// <returns>
        ///     The configured route handler builder.
        /// </returns>
        public RouteHandlerBuilder ProducesPatch<T>(bool hasValidation = true)
        {
            builder = builder
                        .Produces<T>()
                        .ProducesProblem(StatusCodes.Status404NotFound)
                        .ProducesProblem(StatusCodes.Status400BadRequest)
                        .ProducesProblem(StatusCodes.Status409Conflict);

            if (hasValidation)
            {
                builder = builder.ProducesValidationProblem();
            }

            return builder;
        }
    }
}
