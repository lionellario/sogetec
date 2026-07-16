import { useState } from "react";
import "./Sidebar.css";
import { Link } from "react-router-dom";
import {
  Book,
  ChevronRight,
  CircleDot,
  LayoutDashboard,
  Summary,
  Users,
} from "lucide-react";
import { IMG_SRC } from "../../lib/Constant";

interface Props {
  collapsed: boolean;
  mobileOpen: boolean;
  onClose: () => void;
}

const menus = [
  {
    title: "Dashboard",
    icon: <LayoutDashboard />,
    children: [],
  },
  {
    title: "Users",
    icon: <Users />,
    children: ["Customers", "Employees"],
  },
  {
    title: "Products",
    icon: <Book />,
    children: ["Inventory", "Categories"],
  },
  {
    title: "Reports",
    icon: <Summary />,
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
          <img
            src={`${IMG_SRC}/sogetec_icon_png_white_600_879.png`}
            alt="Logo"
            className="logo"
          />
          <span className="company">Sogetec</span>
        </div>

        <div className="menu-list">
          {menus.map((item, index) => (
            <div key={item.title}>
              <div
                className={`menu-item${expanded === index ? " menu-active" : ""}`}
                onClick={() => setExpanded(expanded === index ? null : index)}
              >
                <Link to="#">
                  <span className="menu-icon">{item.icon}</span>
                  <span className="menu-text">{item.title}</span>
                  {item.children.length > 0 && (
                    <span
                      className={`arrow${expanded === index ? " expanded" : ""}`}
                    >
                      <ChevronRight />
                    </span>
                  )}
                </Link>
              </div>
              <div
                className={`submenu${expanded === index ? " submenu-open" : ""}`}
              >
                {item.children.map((child) => (
                  <div key={child} className="submenu-item">
                    <Link to="#">
                      <span className="submenu-icon">
                        <CircleDot />
                      </span>
                      <p>{child}</p>
                    </Link>
                  </div>
                ))}
              </div>
            </div>
          ))}
        </div>
      </aside>
      {mobileOpen && <div className="overlay" onClick={onClose} />}
    </>
  );
}
