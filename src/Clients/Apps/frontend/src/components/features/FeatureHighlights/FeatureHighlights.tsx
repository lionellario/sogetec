import {
    FiMessageCircle,
    FiRefreshCw,
    FiShield,
    FiTruck,
} from "react-icons/fi";

import "./FeatureHighlights.css";

export default function FeatureHighlights() {
  return (
    <section className="features">
      <div className="feature">
        <FiTruck />
        <div>
          <h4>Free Shipping</h4>
          <p>Orders over $199</p>
        </div>
      </div>

      <div className="feature">
        <FiShield />
        <div>
          <h4>Secure Payment</h4>
          <p>Protected checkout</p>
        </div>
      </div>

      <div className="feature">
        <FiRefreshCw />
        <div>
          <h4>Money Back</h4>
          <p>30 day guarantee</p>
        </div>
      </div>

      <div className="feature">
        <FiMessageCircle />
        <div>
          <h4>Support</h4>
          <p>24/7 service</p>
        </div>
      </div>
    </section>
  );
}
