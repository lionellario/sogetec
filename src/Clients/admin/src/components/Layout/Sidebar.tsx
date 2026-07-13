import { useState } from "react";
import "./Sidebar.css";

interface Props {
  collapsed: boolean;

  mobileOpen: boolean;

  onClose: () => void;
}

const menus = [
  {
    title: "Dashboard",
    icon: "📊",
    children: [],
  },
  {
    title: "Users",
    icon: "👥",
    children: ["Customers", "Employees"],
  },
  {
    title: "Products",
    icon: "📦",
    children: ["Inventory", "Categories"],
  },
  {
    title: "Reports",
    icon: "📈",
    children: [],
  },
];

export default function Sidebar({ mobileOpen, collapsed, onClose }: Props) {
  const [expanded, setExpanded] = useState<number | null>(null);
  const [hoverExpanded, setHoverExpanded] = useState(false);

  return (
    <>
      <aside
        className={`sidebar${collapsed ? " collapsed" : ""}${hoverExpanded ? " hover-expanded" : ""}${mobileOpen ? " mobile-open" : ""}`}
        onMouseEnter={() => {
          if (collapsed) {
            setHoverExpanded(true);
          }
        }}
        onMouseLeave={() => {
          if (collapsed) {
            setHoverExpanded(false);
          }
        }}
      >
        <div className="sidebar-logo">
          <span className="logo">S</span>

          <span className="company">Savyris</span>
        </div>

        <div className="menu-list">
          {menus.map((item, index) => (
            <div key={item.title}>
              <div
                className="menu-item"
                onClick={() => setExpanded(expanded === index ? null : index)}
              >
                <span className="menu-icon">{item.icon}</span>

                <span className="menu-text">{item.title}</span>

                {item.children.length > 0 && (
                  <span
                    className={`arrow 
                                    ${expanded === index ? "rotate" : ""}`}
                  >
                    ▶
                  </span>
                )}
              </div>

              {expanded === index && (
                <div className="submenu">
                  {item.children.map((x) => (
                    <div key={x} className="submenu-item">
                      {x}
                    </div>
                  ))}
                </div>
              )}
            </div>
          ))}
        </div>
      </aside>

      {mobileOpen && <div className="overlay" onClick={onClose} />}
    </>
  );
}
