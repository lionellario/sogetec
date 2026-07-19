import { useState } from "react";
import Sidebar from "./Sidebar";
import Header from "./Header";
import DashboardPage from "../../pages/Dashboard";
import Footer from "./Footer";
import "./AdminLayout.css";
import type { UserProfile } from "../../lib/profile";
import { Outlet, Route, Routes } from "react-router-dom";
import BrandPage from "../../pages/BrandPage";
import BrandPageEdit from "../../pages/BrandPageEdit";

interface AdminLayoutProps {
  profile: UserProfile | undefined;
  logout: () => void;
}

export default function AdminLayout({ profile, logout }: AdminLayoutProps) {
  const [sidebarCollapsed, setSidebarCollapsed] = useState(false);
  const [mobileSidebarOpen, setMobileSidebarOpen] = useState(false);

  return (
    <div className="admin-layout">
      <Sidebar
        collapsed={sidebarCollapsed}
        mobileOpen={mobileSidebarOpen}
        onClose={() => setMobileSidebarOpen(false)}
      />

      <div
        className={`admin-main${sidebarCollapsed ? " sidebar-collapsed" : ""}`}
      >
        <Header
          profile={profile}
          logout={logout}
          toggleSidebar={() => {
            if (window.innerWidth <= 1024) {
              setMobileSidebarOpen((previous) => !previous);
            } else {
              setSidebarCollapsed((previous) => !previous);
            }
          }}
        />

        <main className="content">
          {/* <Routes>
            <Route path="/" element={<DashboardPage />} />
            <Route path="brands" element={<BrandPage />}>
              <Route path="create" element={<BrandPageEdit />} />
            </Route>
            <Route path="*" element={<h1>404</h1>} />
          </Routes> */}
          <Outlet />
        </main>

        <Footer />
      </div>
    </div>
  );
}
