import "./App.css";
import { AUTH_ERROR_CODE } from "./enums/authErrorCode";
import { useKeycloak } from "./hooks/useKeycloak";
import { getValidToken } from "./lib/keycloak";
import AdminLayout from "./components/Layout/AdminLayout";
import { BrowserRouter, Route, Routes } from "react-router-dom";

function App() {
  const { isAuthenticated, isLoading, profile, error, login, logout } =
    useKeycloak();

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

  const printToken = async (event: any) => {
    event.preventDefault();
    const token = await getValidToken();

    if (!token) {
      return;
    }

    console.log(token);
  };

  return (
    <div className="container">
      {!isAuthenticated ? (
        <div>
          <h1 className="title">Savyris Platform</h1>
          <p className="subtitle">
            Secure financial operations with enterprise-grade authentication
          </p>
          <p>
            <button className="btn" onClick={printToken}>
              Get new Token
            </button>
          </p>
          <div className="card">
            <h2>Welcome</h2>
            <h2>You're not authenticated</h2>

            <button className="btn login" onClick={login}>
              Login to your organization
            </button>
          </div>
        </div>
      ) : (
        <BrowserRouter>
          <Routes>
            <Route
              path="/"
              element={<AdminLayout profile={profile} logout={logout} />}
            />
            <Route path="*" element={<h1>404</h1>} />
          </Routes>
        </BrowserRouter>
      )}
    </div>
  );
}

export default App;
