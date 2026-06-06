import type { Product } from "../../../data/products";

import "./ProductCard.css";

interface Props {
  product: Product;
}

export default function ProductCard({ product }: Props) {
  return (
    <article className="product-card">
      {product.sale && <span className="sale-badge">Sale</span>}

      <img src={product.image} alt={product.name} />

      <h4>{product.name}</h4>

      <div className="price">${product.price}</div>
    </article>
  );
}
