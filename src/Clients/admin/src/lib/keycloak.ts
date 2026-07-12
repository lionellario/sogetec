import Keycloak, {
  type KeycloakLoginOptions,
  type KeycloakTokenParsed,
} from "keycloak-js";
import { AUTH_ERROR_CODE } from "../enums/authErrorCode";

const isDev = import.meta.env.MODE === "development";

const keycloakInstance: Keycloak = new Keycloak({
  url: isDev
    ? import.meta.env.VITE_KEYCLOAK_HTTP ||
      import.meta.env.VITE_KEYCLOAK_HTTPS ||
      ""
    : import.meta.env.VITE_KEYCLOAK_HTTPS ||
      import.meta.env.VITE_KEYCLOAK_HTTP ||
      "",
  realm: import.meta.env.VITE_KEYCLOAK_REALM,
  clientId: import.meta.env.VITE_KEYCLOAK_CLIENT_ID,
});

let initialized = false;

/**
 * Force logout safely
 */
export const forceLogout = () => {
  keycloakInstance.logout({
    redirectUri: window.location.origin,
  });
};

export const initKeycloak = async () => {
  if (initialized) return keycloakInstance;

  await keycloakInstance.init({
    onLoad: "check-sso",
    pkceMethod: "S256",
    checkLoginIframe: false,
    silentCheckSsoRedirectUri:
      window.location.origin + "/silent-check-sso.html",
  });

  initialized = true;

  keycloakInstance.onTokenExpired = async () => {
    try {
      await keycloakInstance.updateToken(30);
    } catch {
      forceLogout();
    }
  };

  return keycloakInstance;
};

export const getKeycloak = () => keycloakInstance;

/**
 * Login with tenant-aware redirect
 */
export const login = async () => {
  try {
    const options: KeycloakLoginOptions | undefined = {
      redirectUri: window.location.href,
    };

    await keycloakInstance.login(options);
  } catch (error) {
    throw new Error(AUTH_ERROR_CODE.KEYCLOAK_LOGIN_FAILED);
  }
};

export const logout = () => {
  keycloakInstance.logout({
    redirectUri: window.location.origin,
  });
};

export const getUserInfo = () => {
  const parsed: KeycloakTokenParsed | undefined = keycloakInstance.tokenParsed;
  if (!parsed) return null;

  return {
    username: parsed.preferred_username,
    email: parsed.email,
    firstName: parsed.given_name,
    lastName: parsed.family_name,
    tenants: parsed.membership || [],
  };
};

/**
 * 🔥 SECURE TOKEN ACCESS
 */
export const getValidToken = async (options?: {
  forceRefresh?: boolean;
}): Promise<string | null> => {
  if (!keycloakInstance.authenticated) {
    return null;
  }

  try {
    const minValidity = options?.forceRefresh ? -1 : 30;
    await keycloakInstance.updateToken(minValidity);

    return keycloakInstance.token ?? null;
  } catch {
    forceLogout();
    return null;
  }
};
