// src/auth/AuthContext.tsx
import React, { createContext, useContext } from "react";
import { useKeycloak } from "../hooks/useKeycloak";
import { getValidToken } from "../lib/keycloak";
import type { UserProfile } from "../lib/profile";

interface AuthContextType {
  isAuthenticated: boolean;
  isLoading: boolean;
  error: string | undefined;
  profile: UserProfile | undefined;
  login: () => Promise<void>;
  logout: () => void;
}

const AuthContext = createContext<AuthContextType | null>(null);

export const AuthProvider: React.FC<{ children: React.ReactNode }> = ({
  children,
}) => {
  const { isAuthenticated, isLoading, profile, error, login, logout } =
    useKeycloak();

  const printToken = async (event: any) => {
    event.preventDefault();
    const token = await getValidToken();

    if (!token) {
      return;
    }

    console.log(token);
  };

  return (
    <AuthContext.Provider
      value={{
        isAuthenticated,
        isLoading,
        profile,
        error,
        login,
        logout,
      }}
    >
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (!context) throw new Error("useAuth must be used within an AuthProvider");
  return context;
};
