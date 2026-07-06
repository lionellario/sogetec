import { useEffect, useState } from "react";
import FeatureHighlights from "../components/features/FeatureHighlights/FeatureHighlights";
import HeroBanner from "../components/hero/HeroBanner/HeroBanner";
import Container from "../components/layout/Container";
import ProductGrid from "../components/product/ProductGrid/ProductGrid";
import api from "../lib/axios";
import { API_PREFIX } from "../lib/Constant";
import "./HomePage.css";

export default function HomePage() {
  const [categories, setCategories] = useState([]);

  useEffect(() => {
    async function loadCategories() {
      const response = await api.get(`${API_PREFIX}/categories`);
      setCategories(response.data);
    }

    loadCategories();
  }, []);

  console.log(categories);
  return (
    <Container>
      <section className="hero-layout">
        <div className="hero-left-flex">
          <p></p>
        </div>
        <HeroBanner />
      </section>
      <FeatureHighlights />
      <ProductGrid />
    </Container>
  );
}
