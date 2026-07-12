import "./HeroBanner.css";

export default function HeroBanner() {
  return (
    <section className="hero">
      <div className="hero__content">
        <p>Virtual reality glasses</p>

        <h1>
          Experience
          <br />
          New Reality
        </h1>

        <button>Shop Now</button>
      </div>

      <div className="hero__image">
        <img src="/hero-vr.png" alt="VR" />
      </div>
    </section>
  );
}
