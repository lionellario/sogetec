import "./App.css";
import { RouterProvider } from "react-router-dom";
import { AuthProvider } from "./auth/AuthContext";
import { router } from "./router";
import { SnackbarProvider } from "notistack";

function App() {
  return (
    <SnackbarProvider maxSnack={3} autoHideDuration={2000}>
      <div className="container">
        <AuthProvider>
          <RouterProvider router={router} />
        </AuthProvider>
      </div>
    </SnackbarProvider>
  );
}

export default App;
