import axios from "axios";
import { forceLogout, getValidToken, validateTenant } from "./keycloak";

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
  const token = await getValidToken({
    forceRefresh: false, // default behavior
  });

  if (!token) {
    forceLogout();
    return Promise.reject("No valid token");
  }

  // 🔥 double check tenant
  if (!validateTenant()) {
    forceLogout();
    return Promise.reject("Tenant mismatch");
  }

  config.headers.Authorization = `Bearer ${token}`;
  return config;
});

/**
 * RESPONSE INTERCEPTOR
 * - handle 401 globally
 */
api.interceptors.response.use(
  (response) => response,
  async (error) => {
    if (error.response?.status === 401) {
      console.warn("401 detected → logout");
      forceLogout();
    }

    return Promise.reject(error);
  },
);

export default api;
