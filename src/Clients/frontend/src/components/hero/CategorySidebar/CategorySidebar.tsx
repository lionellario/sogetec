import { useState } from "react";
import { FiChevronDown, FiChevronRight, FiGrid } from "react-icons/fi";
import type { CategoryGroup } from "../../../lib/Category";
import "./CategorySidebar.css";
import { Link } from "react-router-dom";

interface SideBarMenuProps {
  categoryGroups: CategoryGroup[];
  alwaysOpen: boolean;
}

export default function CategorySidebar({
  categoryGroups,
  alwaysOpen,
}: SideBarMenuProps) {
  const [isOpen, setIsOpen] = useState(false);
  const [activeGroup, setActiveGroup] = useState<CategoryGroup | null>(
    null,
  );

  const handleMenuLeave = () => {
    setIsOpen(false);
  };
  
  return (
    <aside className="category-sidebar" onMouseLeave={handleMenuLeave}>
      <div
        className={`sidebar-title ${isOpen ? "active" : ""}`}
        onMouseEnter={() => {
          setIsOpen(true);
          setActiveGroup(null);
        }}
      >
        <div className="sidebar-title-text">
          <FiGrid />
          <span>Catalog</span>
        </div>
        <span className="arrow-down">
          <FiChevronDown />
        </span>
      </div>

      {(isOpen || alwaysOpen) && (
        <div className="sidebar-menu">
          <div
            className="sidebar-list"
            onMouseLeave={(e) => {
              const nextTarget = e.relatedTarget as HTMLElement;
              if (!nextTarget || !nextTarget.closest(".mega-box")) {
                setActiveGroup(null);
              }
            }}
          >
            {categoryGroups.map((group: CategoryGroup) => (
              <div
                key={group.id}
                className={`category-item ${activeGroup?.id === group.id ? "selected" : ""}`}
                onMouseEnter={() => setActiveGroup(group)}
              >
                <span>{group.name}</span>
                <FiChevronRight className="arrow-right" />
              </div>
            ))}
          </div>

          {/* Dynamic Mega Box Panel */}
          {activeGroup && (
            <div
              className="mega-box"
              onMouseLeave={(e) => {
                const nextTarget = e.relatedTarget as HTMLElement;
                if (!nextTarget || !nextTarget.closest(".sidebar-list")) {
                  setActiveGroup(null);
                }
              }}
            >
              <div className="mega-columns-container">
                {activeGroup.categories.map((category) => (
                  <div key={category.id} className="mega-column">
                    <h3 className="column-title">{category.name}</h3>
                    <ul className="column-links">
                      {category.children.map((item) => (
                        <li key={item.id}>
                          <Link to={`/categories/${item.slug}`}>{item.name}</Link>
                        </li>
                      ))}
                    </ul>
                  </div>
                ))}
              </div>

              {/* Image Column */}
              <div className="mega-image-column">
                <img
                  src={activeGroup.imageUrl}
                  alt={activeGroup.name}
                  className="mega-menu-image"
                />
              </div>
            </div>
          )}
        </div>
      )}
    </aside>
  );
}
