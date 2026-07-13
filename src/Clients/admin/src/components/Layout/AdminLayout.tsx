import { useState } from "react";
import Sidebar from "./Sidebar";
import Header from "./Header";
import Dashboard from "./Dashboard";
import Footer from "./Footer";

import "./AdminLayout.css";

export default function AdminLayout({ profile, logout }: any) {
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
          <Dashboard />
        </main>

        <Footer />
      </div>
    </div>
  );
}
