import { useState } from "react";
import { FiChevronDown, FiChevronRight, FiGrid } from "react-icons/fi";
import type { MegaMenuCategory } from "../../../lib/MenuCategory";
import "./CategorySidebar.css";

interface SideBarMenuProps {
  categories: MegaMenuCategory[];
  alwaysOpen: boolean;
}

export default function CategorySidebar({
  categories,
  alwaysOpen,
}: SideBarMenuProps) {
  const [isOpen, setIsOpen] = useState(false);
  const [activeCategory, setActiveCategory] = useState<MegaMenuCategory | null>(
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
          setActiveCategory(null);
        }}
      >
        <div className="sidebar-title-text">
          <FiGrid />
          <span>Categories</span>
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
                setActiveCategory(null);
              }
            }}
          >
            {categories.map((category) => (
              <div
                key={category.id}
                className={`category-item ${activeCategory?.id === category.id ? "selected" : ""}`}
                onMouseEnter={() => setActiveCategory(category)}
              >
                <span>{category.title}</span>
                <FiChevronRight className="arrow-right" />
              </div>
            ))}
          </div>

          {/* Dynamic Mega Box Panel */}
          {activeCategory && (
            <div
              className="mega-box"
              onMouseLeave={(e) => {
                const nextTarget = e.relatedTarget as HTMLElement;
                if (!nextTarget || !nextTarget.closest(".sidebar-list")) {
                  setActiveCategory(null);
                }
              }}
            >
              <div className="mega-columns-container">
                {activeCategory.groups.map((group) => (
                  <div key={group.id} className="mega-column">
                    <h3 className="column-title">{group.title}</h3>
                    <ul className="column-links">
                      {group.items.map((item) => (
                        <li key={item.id}>
                          <a href={item.url}>{item.name}</a>
                        </li>
                      ))}
                    </ul>
                  </div>
                ))}
              </div>

              {/* Image Column */}
              <div className="mega-image-column">
                <img
                  src={activeCategory.imageUrl}
                  alt={activeCategory.imageAlt}
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
