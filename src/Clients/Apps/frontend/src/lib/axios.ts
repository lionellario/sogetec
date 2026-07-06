import axios from "axios";

const api = axios.create({
  baseURL: import.meta.env.VITE_SOGETEC_API_HTTPS, // https://api.savyris.com
  withCredentials: true,
});

/**
 * REQUEST INTERCEPTOR
 * - attach token
 * - enforce tenant BEFORE request
 */
api.interceptors.request.use(async (config) => {
  return config;
});

/**
 * RESPONSE INTERCEPTOR
 * - handle 401 globally
 */
api.interceptors.response.use(
  (response) => {
    if (import.meta.env.DEV) {
      console.debug(
        `[${response.status}] ${response.config.method?.toUpperCase()} ${response.config.url}`,
      );
    }

    return response;
  },
  async (error) => {
    // Network failure (no HTTP response received)
    if (!error.response) {
      console.error("Network error:", error.message);

      return Promise.reject({
        status: 0,
        type: "network",
        message: "Unable to reach the server.",
      });
    }

    const { status, data, headers } = error.response;

    switch (status) {
      case 400:
      case 409:
      case 422:
        break;

      case 401:
        console.warn("Unauthorized. Logging out.");
        break;

      case 403:
        console.warn("Forbidden.");
        break;

      case 404:
        console.warn("Resource not found.");
        break;

      case 429:
        console.warn(
          `Rate limited. Retry after ${headers["retry-after"] ?? "unknown"} seconds.`,
        );
        break;

      case 500:
        console.error("Internal server error.");
        break;

      case 502:
      case 503:
      case 504:
        console.error("Temporary server error.");
        break;

      default:
        console.error(`Unhandled HTTP status: ${status}`);
    }

    return Promise.reject({
      status,
      message: data?.message ?? error.message,
      traceId: data?.traceId ?? headers["x-request-id"],
      details: data,
    });
  },
);

export default api;
