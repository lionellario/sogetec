import { useEffect, useState } from "react";
import { BrowserRouter, Route, Routes } from "react-router";
import Header from "./components/layout/Header/Header";
import AboutUsPage from "./pages/AboutPage";
import AboutPage1 from "./pages/AboutPage1";
import ContactPage from "./pages/ContactPage";
import HomePage from "./pages/HomePage";
import "./styles/App.css";

export default function App() {
  const [darkMode, setDarkMode] = useState(false);

  useEffect(() => {
    document.documentElement.dataset.theme = darkMode ? "dark" : "light";
  }, [darkMode]);

  return (
    <BrowserRouter>
      <Header
        darkMode={darkMode}
        onToggleTheme={() => setDarkMode((x) => !x)}
      />
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/about" element={<AboutUsPage />} />
        <Route path="/about1" element={<AboutPage1 />} />
        <Route path="/contact" element={<ContactPage />} />
      </Routes>
    </BrowserRouter>
  );
}
