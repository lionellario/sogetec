using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Sogetec.Chassis.Utilities.Network;

public static class HttpClientExtensions
{
    private static readonly JsonSerializerOptions _defaultJsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    private static HttpContent CreateHttpContent(
        object body,
        string mediaType,
        JsonSerializerOptions? jsonOptions)
    {
        return mediaType switch
        {
            "application/json" =>
                JsonContent.Create(
                    body,
                    options: jsonOptions),

            "application/x-www-form-urlencoded" =>
                body is IEnumerable<KeyValuePair<string, string>> form
                    ? new FormUrlEncodedContent(form)
                    : body is Dictionary<string, string> dict
                        ? new FormUrlEncodedContent(dict)
                        : throw new InvalidOperationException("Body must be IEnumerable<KeyValuePair<string,string>>"),

            "text/plain" =>
                new StringContent(
                    body.ToString() ?? string.Empty,
                    Encoding.UTF8,
                    mediaType),

            _ =>
                new StringContent(
                    JsonSerializer.Serialize(
                        body,
                        jsonOptions ?? _defaultJsonOptions),
                    Encoding.UTF8,
                    mediaType)
        };
    }

    extension(HttpResponseMessage response)
    {
        public async Task EnsureSuccessStatusCodeAsync()
        {
            if (response.IsSuccessStatusCode)
                return;

            var content = await response.Content.ReadAsStringAsync();

            throw new InvalidOperationException(
                $"{response.RequestMessage?.Method} " +
                $"Request to {response.RequestMessage?.RequestUri} failed with status code {response.StatusCode}. " +
                $"Response content: {content}");
        }
    }

    extension(HttpClient httpClient)
    {
        public async Task<TResponse?> SendAsync<TResponse>(
            HttpMethod method,
            string url,
            string? bearerToken = null,
            object? body = null,
            string mediaType = "application/json",
            Dictionary<string, string>? headers = null,
            JsonSerializerOptions? jsonOptions = null,
            HttpCompletionOption completionOption = HttpCompletionOption.ResponseHeadersRead,
            Action<HttpResponseMessage>? onResponse = null,
            CancellationToken cancellationToken = default)
        {
            using var response = await httpClient.BuildAndSendRequestAsync(
                method,
                url,
                bearerToken,
                body,
                mediaType,
                headers,
                jsonOptions,
                completionOption,
                cancellationToken);

            await response.EnsureSuccessStatusCodeAsync();

            onResponse?.Invoke(response);

            await using var stream =
                await response.Content.ReadAsStreamAsync(cancellationToken);

            return await JsonSerializer.DeserializeAsync<TResponse>(
                stream,
                jsonOptions ?? _defaultJsonOptions,
                cancellationToken);
        }

        public async Task<JsonDocument> SendAsJsonDocumentAsync(
            HttpMethod method,
            string url,
            string? bearerToken = null,
            object? body = null,
            string mediaType = "application/json",
            Dictionary<string, string>? headers = null,
            JsonSerializerOptions? jsonOptions = null,
            HttpCompletionOption completionOption = HttpCompletionOption.ResponseHeadersRead,
            Action<HttpResponseMessage>? onResponse = null,
            CancellationToken cancellationToken = default)
        {
            using var response = await httpClient.BuildAndSendRequestAsync(
                method,
                url,
                bearerToken,
                body,
                mediaType,
                headers,
                jsonOptions,
                completionOption,
                cancellationToken);

            await response.EnsureSuccessStatusCodeAsync();

            onResponse?.Invoke(response);

            await using var stream =
                await response.Content.ReadAsStreamAsync(cancellationToken);

            return await JsonDocument.ParseAsync(
                stream,
                cancellationToken: cancellationToken
            );
        }

        public async Task SendAsync(
            HttpMethod method,
            string url,
            string? bearerToken = null,
            object? body = null,
            string mediaType = "application/json",
            Dictionary<string, string>? headers = null,
            JsonSerializerOptions? jsonOptions = null,
            HttpCompletionOption completionOption = HttpCompletionOption.ResponseHeadersRead,
            Action<HttpResponseMessage>? onResponse = null,
            CancellationToken cancellationToken = default)
        {
            using var response = await httpClient.BuildAndSendRequestAsync(
                method,
                url,
                bearerToken,
                body,
                mediaType,
                headers,
                jsonOptions,
                completionOption,
                cancellationToken);

            await response.EnsureSuccessStatusCodeAsync();

            onResponse?.Invoke(response);
        }

        private async Task<HttpResponseMessage> BuildAndSendRequestAsync(
            HttpMethod method,
            string url,
            string? bearerToken = null,
            object? body = null,
            string mediaType = "application/json",
            Dictionary<string, string>? headers = null,
            JsonSerializerOptions? jsonOptions = null,
            HttpCompletionOption completionOption = HttpCompletionOption.ResponseHeadersRead,
            CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage(method, url);

            try
            {
                if (!string.IsNullOrWhiteSpace(bearerToken))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
                }

                if (headers is not null)
                {
                    foreach (var (key, value) in headers)
                    {
                        request.Headers.TryAddWithoutValidation(key, value);
                    }
                }

                if (body is not null)
                {
                    request.Content = CreateHttpContent(
                        body,
                        mediaType,
                        jsonOptions);
                }

                return await httpClient.SendAsync(
                    request,
                    completionOption,
                    cancellationToken);
            }
            finally
            {
                request.Dispose();
            }
        }
    }
}
