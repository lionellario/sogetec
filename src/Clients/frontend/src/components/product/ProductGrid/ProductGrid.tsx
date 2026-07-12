import { products } from "../../../data/products";
import ProductCard from "../ProductCard/ProductCard";

import "./ProductGrid.css";

export default function ProductGrid() {
  return (
    <section className="product-grid">
      {products.map((product) => (
        <ProductCard key={product.id} product={product} />
      ))}
    </section>
  );
}
