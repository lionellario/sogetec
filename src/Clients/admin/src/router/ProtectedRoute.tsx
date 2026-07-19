// src/routes/ProtectedRoute.tsx
import { Outlet } from "react-router-dom";
import { useAuth } from "../auth/AuthContext";
import { AUTH_ERROR_CODE } from "../enums/authErrorCode";

export const ProtectedRoute: React.FC = () => {
  const { isAuthenticated, isLoading, error, login } = useAuth();

  if (isLoading) {
    return (
      <div className="center">
        <p>Initializing security...</p>
      </div>
    );
  }

  if (
    error === AUTH_ERROR_CODE.KEYCLOAK_INIT_FAILED ||
    error === AUTH_ERROR_CODE.KEYCLOAK_LOGIN_FAILED
  ) {
    return (
      <div className="container">
        <div className="card error">
          <h2>Authentication error</h2>
          <p>Please try again later.</p>
        </div>
      </div>
    );
  }

  if (!isAuthenticated) {
    // Keycloak handles its own login page UI, so we trigger their login sequence here
    return (
      <div>
        <h1 className="title">Savyris Platform</h1>
        <p className="subtitle">
          Secure financial operations with enterprise-grade authentication
        </p>
        <div className="card">
          <h2>Welcome</h2>
          <h2>You're not authenticated</h2>

          <button className="btn login" onClick={login}>
            Login to your organization
          </button>
        </div>
      </div>
    );
  }

  // Renders the matched child route layout
  return <Outlet />;
};
