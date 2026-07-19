import { useEffect, useRef, useState } from "react";
import { AUTH_ERROR_CODE } from "../enums/authErrorCode";
import {
  getUserInfo,
  initKeycloak,
  login as kcLogin,
  logout,
} from "../lib/keycloak";
import type { UserProfile } from "../lib/profile";

type AuthState = {
  isAuthenticated: boolean;
  isLoading: boolean;
  profile?: UserProfile;
  error?: string;
};

export const useKeycloak = () => {
  const [state, setState] = useState<AuthState>({
    isAuthenticated: false,
    isLoading: true,
  });

  const isInitialized = useRef(false);

  useEffect(() => {
    if (isInitialized.current) return;
    isInitialized.current = true;

    initKeycloak()
      .then((keycloakInstance) => {
        let user: any;

        if (keycloakInstance.authenticated) {
          user = getUserInfo();
        }

        setState((prev) => ({
          ...prev,
          isAuthenticated: !!keycloakInstance.authenticated,
          isLoading: false,
          profile: user,
        }));
      })
      .catch(() => {
        setState((prev) => ({
          ...prev,
          isAuthenticated: false,
          isLoading: false,
          error: AUTH_ERROR_CODE.KEYCLOAK_INIT_FAILED,
        }));
      });
  }, []);

  /**
   * 🔥 Wrapped login with error handling
   */
  const login = async () => {
    try {
      await kcLogin();
    } catch {
      setState((prev) => ({
        ...prev,
        error: AUTH_ERROR_CODE.KEYCLOAK_LOGIN_FAILED,
      }));
    }
  };

  return {
    ...state,
    login,
    logout,
  };
};
