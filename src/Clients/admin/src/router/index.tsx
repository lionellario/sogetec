import { createBrowserRouter } from "react-router-dom";
import AdminLayout from "../components/Layout/AdminLayout";
import BrandEditPage from "../pages/BrandEditPage";
import BrandPage from "../pages/BrandPage";
import CategoryGroupEditPage from "../pages/CategoryGroupEditPage";
import CategoryGroupPage from "../pages/CategoryGroupPage";
import DashboardPage from "../pages/Dashboard";
import { ProtectedRoute } from "./ProtectedRoute";

const NotFound = () => <h2>404 - Page Not Found</h2>;

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
            element: <BrandEditPage />,
          },
          {
            path: "brands/edit",
            element: <BrandEditPage />,
          },
          {
            path: "category-groups",
            element: <CategoryGroupPage />,
          },
          {
            path: "category-groups/create",
            element: <CategoryGroupEditPage />,
          },
          {
            path: "category-groups/edit",
            element: <CategoryGroupEditPage />,
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
