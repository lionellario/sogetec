import {
  FiHeart,
  FiMoon,
  FiSearch,
  FiShoppingCart,
  FiSun,
  FiUser,
} from "react-icons/fi";
import { Link } from "react-router";
import { useIsHomePage } from "../../../hooks/useIsHomePage";
import { API_PREFIX, IMG_SRC } from "../../../lib/Constant";
import CategorySidebar from "../../hero/CategorySidebar/CategorySidebar";
import "./Header.css";
import { useEffect, useState } from "react";
import api from "../../../lib/axios";
import { buildCategoryTree, type CategoryGroup } from "../../../lib/Category";

interface Props {
  darkMode: boolean;
  onToggleTheme: () => void;
}

export default function Header({ darkMode, onToggleTheme }: Props) {
  const isHome = useIsHomePage();

  const [categories, setCategories] = useState<CategoryGroup[]>([]);

  useEffect(() => {
    async function loadCategories() {
      const response = await api.get(`${API_PREFIX}/categories`);
      const groups = buildCategoryTree(response.data);
      setCategories(groups);
    }

    loadCategories();
  }, []);

  return (
    <header className="header">
      <div className="container">
        <div className="hd-top">
          <div className="header__logo">
            <img
              src={`${IMG_SRC}/sogetec_icon_png_white_600_879.png`}
              alt="Logo"
            />
            <span> Sogetec</span>
          </div>
          <div className="header__search">
            <input placeholder="Search products..." />
            <FiSearch />
          </div>
          <div className="header__actions">
            <button onClick={onToggleTheme}>
              {darkMode ? <FiSun /> : <FiMoon />}
            </button>

            <FiHeart />
            <FiUser />
            <FiShoppingCart />
          </div>
        </div>
        <div className="hd-bottom">
          <CategorySidebar categoryGroups={categories} alwaysOpen={isHome} />
          <div>
            <nav className="nav-links">
              <Link to="/">Home</Link>
              <Link to="/about">About</Link>
              <Link to="/contact">Contact</Link>
              <Link to="/blog">Blog</Link>
            </nav>
          </div>
        </div>
      </div>
    </header>
  );
}
