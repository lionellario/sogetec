import { createBrowserRouter } from "react-router-dom";
import { ProtectedRoute } from "./ProtectedRoute";
import DashboardPage from "../pages/Dashboard";
import BrandPage from "../pages/BrandPage";
import BrandPageEdit from "../pages/BrandPageEdit";
import AdminLayout from "../components/Layout/AdminLayout";

const NotFound = () => <h2>404 - Page Not Found ----</h2>;

export const router = createBrowserRouter([
  {
    element: <ProtectedRoute />,
    children: [
      {
        path: "/",
        element: <AdminLayout />,
        children: [
          {
            index: true,
            element: <DashboardPage />,
          },
          {
            path: "brands",
            element: <BrandPage />,
          },
          {
            path: "brands/create",
            element: <BrandPageEdit />,
          },
          {
            path: "brands/edit",
            element: <BrandPageEdit />,
          },
        ],
      },
    ],
  },
  {
    path: "*",
    element: <NotFound />,
  },
]);
