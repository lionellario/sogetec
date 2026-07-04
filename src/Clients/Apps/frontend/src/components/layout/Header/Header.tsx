import {
  FiHeart,
  FiMoon,
  FiSearch,
  FiShoppingCart,
  FiSun,
  FiUser,
} from "react-icons/fi";
import { Link } from "react-router";
import { mockMenuData } from "../../../data/categories";
import { useIsHomePage } from "../../../hooks/useIsHomePage";
import { IMG_SRC } from "../../../lib/Constant";
import CategorySidebar from "../../hero/CategorySidebar/CategorySidebar";
import "./Header.css";

interface Props {
  darkMode: boolean;
  onToggleTheme: () => void;
}

export default function Header({ darkMode, onToggleTheme }: Props) {
  const isHome = useIsHomePage();

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
          <CategorySidebar categories={mockMenuData} alwaysOpen={isHome} />
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
