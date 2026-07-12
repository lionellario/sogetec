import { useState } from "react";
import "./App.css";
import { AUTH_ERROR_CODE } from "./enums/authErrorCode";
import { useKeycloak } from "./hooks/useKeycloak";
import { getValidToken } from "./lib/keycloak";
import "./styles/custom.css";

function App() {
  const [count, setCount] = useState(0);

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
      <h1 className="title">Savyris Platform</h1>
      <p className="subtitle">
        Secure financial operations with enterprise-grade authentication
      </p>
      <p>
        <button className="btn" onClick={printToken}>
          Get new Token
        </button>
      </p>

      {!isAuthenticated ? (
        <div className="card">
          <h2>Welcome</h2>
          <h2>You're not authenticated</h2>

          <button className="btn login" onClick={login}>
            Login to your organization
          </button>
        </div>
      ) : (
        <div>
          <div className="card success">
            <h2>Welcome {profile?.firstName} 👋</h2>
            <p>{profile?.email}</p>

            <section id="next-steps">
              <button
                className="counter"
                onClick={() => setCount((count) => count + 1)}
              >
                Count is {count}
              </button>
            </section>

            <p>
              <strong>Username:</strong> {profile?.username}
            </p>
            <p>
              <strong>Email:</strong> {profile?.email}
            </p>

            <button className="btn logout" onClick={logout}>
              Logout
            </button>
          </div>
        </div>
      )}
    </div>
  );
}

export default App;
