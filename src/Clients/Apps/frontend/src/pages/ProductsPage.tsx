import { useEffect, useState } from "react";
import api from "../lib/axios";

function ProductsPage() {
  const [products, setProducts] = useState([]);

  useEffect(() => {
    async function loadProducts() {
      const response = await api.get("/api/products");
      setProducts(response.data);
    }

    loadProducts();
  }, []);

  return (
    <div>
      {products.map((p: any) => (
        <div key={p.id}>{p.name}</div>
      ))}
    </div>
  );
}
