import { useState } from "react";
import "./Header.css";

interface Props {
  toggleSidebar: () => void;
  profile: any;
  logout: any;
}

export default function Header({ toggleSidebar, profile, logout }: Props) {
  const [userMenuOpen, setUserMenuOpen] = useState(false);

  return (
    <header className="header">
      <button className="hamburger" onClick={toggleSidebar}>
        ☰
      </button>

      <div className="mobile-logo">Savyris</div>

      <div className="user-area">
        <span>{profile?.username}</span>

        <span onClick={logout}>Logout</span>

        <span>⚙</span>
      </div>

      <button
        className="mobile-user"
        onClick={() => setUserMenuOpen((previous) => !previous)}
      >
        👤
      </button>

      {userMenuOpen && (
        <div className="mobile-user-panel">
          <div>John Doe</div>

          <div onClick={logout}>Logout</div>

          <div>⚙ Settings</div>
        </div>
      )}
    </header>
  );
}
