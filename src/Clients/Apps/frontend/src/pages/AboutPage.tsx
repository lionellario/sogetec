import React from "react";
import Container from "../components/layout/Container";
import { IMG_SRC } from "../lib/Constant";
import "./AboutPage.css";

// type StatCounterProps = {
//   value: number;
//   suffix?: string;
//   label: string;
// };

// const StatCounter: React.FC<StatCounterProps> = ({
//   value,
//   suffix = "",
//   label,
// }) => {
//   const [count, setCount] = useState(0);
//   const ref = useRef<HTMLDivElement | null>(null);
//   const [visible, setVisible] = useState(false);

//   useEffect(() => {
//     const element = ref.current;

//     if (!element) return;

//     const observer = new IntersectionObserver(
//       ([entry]) => {
//         if (entry.isIntersecting) {
//           setVisible(true);
//           observer.disconnect();
//         }
//       },
//       {
//         threshold: 0.3,
//       },
//     );

//     observer.observe(element);

//     return () => observer.disconnect();
//   }, []);

//   useEffect(() => {
//     if (!visible) return;

//     let current = 0;
//     const increment = value / 60;

//     const timer = setInterval(() => {
//       current += increment;

//       if (current >= value) {
//         setCount(value);
//         clearInterval(timer);
//       } else {
//         setCount(Math.floor(current));
//       }
//     }, 25);

//     return () => clearInterval(timer);
//   }, [visible, value]);

//   return (
//     <div ref={ref} className="stats-card">
//       <h3>
//         {count}
//         {suffix}
//       </h3>
//       <p>{label}</p>
//     </div>
//   );
// };

const AboutUsPage: React.FC = () => {
  // const expertise = [
  //   {
  //     icon: "⚡",
  //     title: "Electrical Distribution",
  //     description:
  //       "Wholesale and retail distribution of premium electrical products and equipment.",
  //   },
  //   {
  //     icon: "🏭",
  //     title: "Industrial Solutions",
  //     description:
  //       "Power systems and industrial electrical infrastructure solutions.",
  //   },
  //   {
  //     icon: "🔋",
  //     title: "Renewable Energy",
  //     description:
  //       "Electrical systems supporting solar and renewable energy projects.",
  //   },
  //   {
  //     icon: "🛠",
  //     title: "Maintenance",
  //     description:
  //       "Preventive and corrective maintenance for critical installations.",
  //   },
  //   {
  //     icon: "🔥",
  //     title: "Fire Detection",
  //     description:
  //       "Fire alarm installation, monitoring, testing and compliance.",
  //   },
  //   {
  //     icon: "📊",
  //     title: "Control Panels",
  //     description:
  //       "Design, assembly and maintenance of electrical control panels.",
  //   },
  // ];

  // const industries = [
  //   "Commercial Buildings",
  //   "Manufacturing",
  //   "Energy & Utilities",
  //   "Infrastructure",
  //   "Renewable Energy",
  //   "Government Projects",
  //   "Industrial Plants",
  //   "Street Lighting",
  // ];

  const advantages = [
    {
      title: "Premium Quality Products",
      description:
        "Every product supplied by Sogetec meets strict quality and reliability standards.",
    },
    {
      title: "Technical Expertise",
      description:
        "Highly trained professionals with extensive field experience and technical knowledge.",
    },
    {
      title: "Fast Response",
      description:
        "Rapid support for urgent maintenance, troubleshooting and repairs.",
    },
    {
      title: "Flexibility",
      description:
        "Solutions tailored to your budget, schedule and operational needs.",
    },
    {
      title: "Safety First",
      description:
        "Strict adherence to electrical safety regulations and industry standards.",
    },
    {
      title: "Long-Term Partnership",
      description:
        "Building trust through reliable service and long-lasting customer relationships.",
    },
  ];

  // const process = [
  //   {
  //     step: "01",
  //     title: "Consultation",
  //     description:
  //       "Understanding your project requirements and technical objectives.",
  //   },
  //   {
  //     step: "02",
  //     title: "Analysis",
  //     description: "Evaluating your needs and designing the optimal solution.",
  //   },
  //   {
  //     step: "03",
  //     title: "Supply & Installation",
  //     description:
  //       "Delivering quality equipment and professional implementation.",
  //   },
  //   {
  //     step: "04",
  //     title: "Testing",
  //     description: "Comprehensive testing and commissioning of systems.",
  //   },
  //   {
  //     step: "05",
  //     title: "Maintenance",
  //     description: "Continuous monitoring, maintenance and technical support.",
  //   },
  // ];

  return (
    <div className="about-page">
      <section className="banner-land-section">
        <Container>
          <div className="banner-land-content">
            <h1>
              Electrical Solutions Built On
              <span> Expertise, Quality & Reliability</span>
            </h1>

            <p>
              Sogetec SARL is a trusted electrical products distributor and
              service provider based in Douala, Cameroon. We deliver
              high-quality electrical equipment, industrial solutions,
              installation services, maintenance, and monitoring for businesses,
              industries, and infrastructure projects.
            </p>

            <div className="banner-land-actions">
              <button className="btn-primary">Read more</button>
              <button className="btn-secondary">Browse Catalog</button>
            </div>
          </div>
        </Container>
      </section>
      <section className="overview-section">
        <Container>
          <div className="overview-card">
            <div className="overview-text-card">
              <span className="overview-section-tag">ABOUT SOGETEC</span>
              <h2>Who We Are</h2>
              <br />
              <p>
                Sogetec SARL is a Cameroonian company headquartered in Douala,
                specializing in electrical solutions for residential,
                commercial, industrial, and infrastructure projects.
              </p>

              <p>
                Our expertise spans the complete electrical ecosystem, from
                supplying electrical products and equipment to executing
                high-quality field works, maintenance programs, monitoring
                solutions, fire alarm systems, and power distribution projects.
              </p>

              <p>
                Through technical excellence, customer-focused service, and
                commitment to quality, we help organizations build safer,
                smarter, and more reliable electrical infrastructures.
              </p>
            </div>

            <div className="overview-image-card">
              <img
                src={`${IMG_SRC}/sogetec_field_engineer.jpg`}
                alt="Electrical engineers"
              />
            </div>
          </div>
        </Container>
      </section>
      <section className="overview-section overview-section-light">
        <Container>
          <div className="overview-card">
            <div className="overview-image-card">
              <img
                src={`${IMG_SRC}/sogetec_magasin_est.png`}
                alt="Electrical engineers"
              />
            </div>
            <div className="overview-text-card">
              <span className="overview-section-tag">SALES</span>
              <h2>Electrical Products & Equipment</h2>
              <p>
                We distribute and supply premium electrical products for
                buildings, industries, infrastructure projects and renewable
                energy systems.
              </p>

              <ul className="feature-list">
                <li>Electrical Cables</li>
                <li>Circuit Breakers</li>
                <li>Switchgear Equipment</li>
                <li>Power Distribution Systems</li>
                <li>Control Panel Components</li>
                <li>Industrial Electrical Equipment</li>
                <li>Building Electrical Equipment</li>
                <li>Street Lighting Solutions</li>
                <li>Renewable Energy Equipment</li>
                <li>Monitoring Equipment</li>
              </ul>

              <br />
              <h2>Ours Partners</h2>
              <div className="overview-partners-list">
                <img
                  src={`${IMG_SRC}/logo/3m-innovation.jpg`}
                  alt="3M Innovation"
                />
                <img src={`${IMG_SRC}/logo/abb-logo-33px.svg`} alt="ABB" />
                <img src={`${IMG_SRC}/logo/alvarion.jpg`} alt="Alvarion" />
                <img src={`${IMG_SRC}/logo/bosch.jpg`} alt="BOSCH" />
                <img src={`${IMG_SRC}/logo/espa.jpg`} alt="ESPA" />
                <img src={`${IMG_SRC}/logo/legrand.jpg`} alt="LEGRAND" />
                <img
                  src={`${IMG_SRC}/logo/logo-dfelectric-light.png`}
                  alt="DF Electric"
                />
                <img
                  src={`${IMG_SRC}/logo/merlin-gerin.jpg`}
                  alt="MERLIN GERIN"
                />
                <img src={`${IMG_SRC}/logo/osram.jpg`} alt="OSRAM" />
                <img src={`${IMG_SRC}/logo/philips-logo.jpg`} alt="PHILIPS" />
                <img
                  src={`${IMG_SRC}/logo/schneider.jpg`}
                  alt="SCHNEIDER Electrics"
                />
              </div>
            </div>
          </div>
        </Container>
      </section>
      <section className="overview-section">
        <Container>
          <div className="overview-card">
            <div className="overview-text-card">
              <span className="overview-section-tag">FIELD SERVICES</span>
              <h2>Professional Electrical Works</h2>
              <p>
                Our team of experienced specialists supports clients in
                planning, installing, maintaining and optimizing electrical
                systems across multiple sectors.
              </p>

              <ul className="feature-list">
                <li>High Voltage Installations</li>
                <li>Low Voltage Installations</li>
                <li>Industrial Electrical Projects</li>
                <li>Commercial Electrical Projects</li>
                <li>Preventive Maintenance</li>
                <li>Corrective Maintenance</li>
                <li>Electrical Compliance Audits</li>
                <li>Fire Detection Systems</li>
                <li>Monitoring & Diagnostics</li>
                <li>Technical Troubleshooting</li>
              </ul>
            </div>

            <div className="overview-image-card">
              <img
                src={`${IMG_SRC}/sogetec_field_work.jpg`}
                alt="Electrical engineers"
              />
            </div>
          </div>
        </Container>
      </section>
      <section className="overview-section overview-section-light">
        <Container>
          <div className="overview-card">
            <div className="overview-text-card">
              <span className="overview-section-tag">WHY CHOOSE US</span>
              <br /> <br />
              <div className="overview-advantages-grid">
                {advantages.map((item) => (
                  <div key={item.title} className="overview-advantage-card">
                    <h3>{item.title}</h3>

                    <p>{item.description}</p>
                  </div>
                ))}
              </div>
            </div>
          </div>
        </Container>
      </section>
      <section className="overview-section">
        <Container>
          <div className="overview-card">
            <div className="overview-text-card">
              <span className="overview-section-tag">CONTACT US</span>
              <h2>Need Reliable Electrical Solutions?</h2>
              <p>
                Whether you need electrical products, domestic & industrial
                installations, maintenance services, and monitoring solutions,
                our team is ready to support your project.
              </p>
              <br />
              <br />
              <div className="banner-land-actions">
                <button className="btn-primary">Contact Our Team</button>
              </div>
              <br />
              <br />
              <div className="overview-contact-card">
                <h3>SOGETEC SARL</h3>
                <div className="overview-contact-grid">
                  <div>
                    <strong>Address</strong>
                    <p>
                      Bessengue, Cinema Eden
                      <br />
                      Douala, Littoral
                      <br />
                      B.P. 11058, Cameroon
                    </p>
                  </div>

                  <div>
                    <strong>Phone</strong>
                    <p>+237 696 139 122</p>
                  </div>

                  <div>
                    <strong>Specialties</strong>
                    <p>
                      Electricity, Power Systems, Control Panels, Maintenance,
                      Monitoring, Renewable Energy, Electrical Networks and
                      Industrial Solutions.
                    </p>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </Container>
      </section>
    </div>
  );
};

export default AboutUsPage;
