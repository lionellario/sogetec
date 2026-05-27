import { Navigate } from "react-router-dom";
import { useKeycloak } from "../hooks/useKeycloak";

export const TenantGuard = ({ children }: any) => {
  const { isAuthenticated, tenantValid, isLoading } = useKeycloak();

  if (isLoading) {
    return <div className="center">Loading...</div>;
  }

  if (!isAuthenticated) {
    return <Navigate to="/" replace />;
  }

  if (!tenantValid) {
    return <Navigate to="/access-denied" replace />;
  }

  return children;
};
