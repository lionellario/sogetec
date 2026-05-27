namespace Sogetec.Chassis.Exceptions;

public static class ProblemExtensions
{
    extension(HttpContext context)
    {
        public static string GetProblemType(int statusCode)
        {
            return statusCode switch
            {
                400 => "https://tools.ietf.org/html/rfc9110#section-15.5.1",
                401 => "https://tools.ietf.org/html/rfc9110#section-15.5.2",
                403 => "https://tools.ietf.org/html/rfc9110#section-15.5.4",
                404 => "https://tools.ietf.org/html/rfc9110#section-15.5.5",
                409 => "https://tools.ietf.org/html/rfc9110#section-15.5.10",
                _ => "https://tools.ietf.org/html/rfc9110#section-15.6.1"
            };
        }

        public Task WriteProblemAsync(
            string? title = null,
            int? statusCode = null,
            string? detail = null,
            IDictionary<string, object?>? extensions = null)
        {
            var code = statusCode ?? StatusCodes.Status500InternalServerError;
            return TypedResults
                    .Problem(
                        title: !string.IsNullOrWhiteSpace(title) ? title : "We made a mistake but we are on it!",
                        statusCode: statusCode ?? StatusCodes.Status500InternalServerError,
                        type: GetProblemType(code),
                        detail: detail,
                        extensions: extensions
                    )
                    .ExecuteAsync(context);
        }
    }
}
