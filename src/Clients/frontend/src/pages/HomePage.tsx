import FeatureHighlights from "../components/features/FeatureHighlights/FeatureHighlights";
import HeroBanner from "../components/hero/HeroBanner/HeroBanner";
import Container from "../components/layout/Container";
import ProductGrid from "../components/product/ProductGrid/ProductGrid";
import "./HomePage.css";

export default function HomePage() {
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
