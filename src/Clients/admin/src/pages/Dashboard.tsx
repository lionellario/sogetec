import { useEffect, useState } from "react";
import api from "../lib/axios";
import { API_PREFIX } from "../lib/Constant";
import "./Dashboard.css";

export default function DashboardPage() {
  const [products, setProducts] = useState([]);

  useEffect(() => {
    async function loadProducts() {
      const response = await api.get(`${API_PREFIX}/products`);
      setProducts(response.data);
    }

    loadProducts();
  }, []);

  console.log(products);

  return (
    <div>
      <h1>Dashboard</h1>

      <div className="cards">
        <div>
          <h3>Users</h3>
          <p>1,245</p>
        </div>

        <div>
          <h3>Revenue</h3>
          <p>$25,000</p>
        </div>

        <div>
          <h3>Orders</h3>
          <p>530</p>
        </div>
      </div>
    </div>
  );
}
