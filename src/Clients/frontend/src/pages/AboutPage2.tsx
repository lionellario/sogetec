import Container from "../components/layout/Container";

// export default function AboutPage() {
//   return (
//     <Container>
//       <h1>About</h1>
//     </Container>
//   );
// }

import {
  ArrowUpRight,
  Building2,
  Clock,
  Cpu,
  Factory,
  Lightbulb,
  MapPin,
  Package,
  Phone,
  ShieldCheck,
  Sun,
  Zap,
} from "lucide-react";

export default function AboutUsPage() {
  return (
    <Container>
      <div className="w-full bg-slate-50 text-slate-900 antialiased font-sans">
        {/* 1. HERO ZONE */}
        <section className="relative overflow-hidden bg-slate-950 py-24 sm:py-32">
          {/* Subtle geometric background overlay */}
          <div className="absolute inset-0 opacity-10 bg-[radial-gradient(#38bdf8_1px,transparent_1px)] [background-size:16px_16px]" />
          <div className="absolute -left-20 -top-20 h-80 w-80 rounded-full bg-sky-500/10 blur-3xl" />
          <div className="absolute right-0 bottom-0 h-96 w-96 rounded-full bg-amber-500/5 blur-3xl" />

          <div className="mx-auto max-w-7xl px-6 lg:px-8 relative z-10">
            <div className="max-w-3xl">
              <span className="inline-flex items-center gap-1.5 rounded-full bg-sky-500/10 px-3 py-1 text-xs font-medium text-sky-400 ring-1 ring-inset ring-sky-500/20 mb-6">
                <Zap className="h-3 w-3 fill-current" /> Based in Douala,
                Cameroon
              </span>
              <h1 className="text-4xl font-extrabold tracking-tight text-white sm:text-6xl">
                Powering Cameroon’s Infrastructure from the Ground Up
              </h1>
              <p className="mt-6 text-lg leading-8 text-slate-300">
                Sogetec-SARL is your premier partner for high-voltage
                engineering, industrial field works, and premium electrical
                wholesale distribution.
              </p>
            </div>
          </div>
        </section>

        {/* 2. CORE OVERVIEW / WHO WE ARE */}
        <section className="py-20 bg-white">
          <div className="mx-auto max-w-7xl px-6 lg:px-8">
            <div className="grid grid-cols-1 gap-12 lg:grid-cols-2 lg:items-center">
              <div>
                <h2 className="text-3xl font-bold tracking-tight text-slate-950 sm:text-4xl">
                  Who We Are
                </h2>
                <div className="mt-6 space-y-6 text-base leading-7 text-slate-600">
                  <p>
                    At{" "}
                    <strong className="font-semibold text-slate-900">
                      Sogetec-SARL
                    </strong>
                    , we specialize in electricity in all its forms. From
                    general domestic wiring to complex high-and-low voltage
                    industrial networks, tertiary systems, and renewable energy
                    integrations, we deliver reliability to Cameroon's growing
                    infrastructure.
                  </p>
                  <p>
                    Operating out of our headquarters in Douala, we pride
                    ourselves on a massive range of certified products, rapid
                    deployment capabilities, and an uncompromising standard of
                    service. Whether you need a bulk equipment supplier or an
                    expert field engineering team, Sogetec is built to deliver.
                  </p>
                </div>
                <div className="mt-8 flex flex-wrap gap-4 border-t border-slate-100 pt-8">
                  <div>
                    <p className="text-2xl font-bold text-slate-950">11-50</p>
                    <p className="text-xs text-slate-500 uppercase tracking-wider font-medium">
                      Specialized Staff
                    </p>
                  </div>
                  <div className="border-l border-slate-200 pl-6">
                    <p className="text-2xl font-bold text-slate-950">100%</p>
                    <p className="text-xs text-slate-500 uppercase tracking-wider font-medium">
                      Compliance Driven
                    </p>
                  </div>
                </div>
              </div>

              {/* Structural Representation Placeholder representing technical delivery */}
              <div className="relative aspect-[4/3] rounded-2xl bg-slate-900 p-8 shadow-xl overflow-hidden flex flex-col justify-end group lg:order-last">
                <div className="absolute inset-0 bg-gradient-to-t from-slate-950 via-slate-900/60 to-transparent z-10" />
                <div className="absolute inset-0 opacity-20 bg-[linear-gradient(to_right,#808080_1px,transparent_1px),linear-gradient(to_bottom,#808080_1px,transparent_1px)] bg-[size:24px_24px]" />
                <div className="relative z-20">
                  <p className="text-xs font-semibold uppercase tracking-wider text-amber-400">
                    Field Operations
                  </p>
                  <h3 className="mt-2 text-xl font-bold text-white">
                    Douala Headquarters & Technical Hub
                  </h3>
                  <p className="mt-1 text-sm text-slate-300">
                    Deploying qualified teams for continuous installation,
                    monitoring, and maintenance.
                  </p>
                </div>
              </div>
            </div>
          </div>
        </section>

        {/* 3. DUAL BUSINESS ENGINES */}
        <section className="py-20 bg-slate-50 border-y border-slate-200">
          <div className="mx-auto max-w-7xl px-6 lg:px-8">
            <div className="text-center max-w-3xl mx-auto mb-16">
              <h2 className="text-3xl font-bold tracking-tight text-slate-950 sm:text-4xl">
                Our Core Operational Channels
              </h2>
              <p className="mt-4 text-base text-slate-600">
                We bridge engineering execution with robust supply-chain
                networks to support both raw product demands and turnkey
                implementations.
              </p>
            </div>

            <div className="grid grid-cols-1 gap-8 lg:grid-cols-2">
              {/* Wholesale Engine */}
              <div className="flex flex-col justify-between rounded-2xl border border-slate-200 bg-white p-8 shadow-sm hover:shadow-md transition-shadow">
                <div>
                  <div className="inline-flex h-12 w-12 items-center justify-center rounded-xl bg-sky-50 text-sky-600 mb-6">
                    <Package className="h-6 w-6" />
                  </div>
                  <h3 className="text-2xl font-bold text-slate-950">
                    Wholesale & Retail Distribution
                  </h3>
                  <p className="text-sm font-medium text-sky-600 mt-1">
                    Certified Equipment, Scalable Supply
                  </p>
                  <p className="mt-4 text-base text-slate-600 leading-relaxed">
                    We are a leading distributor and wholesaler of premium
                    electrical products and equipment across Cameroon. Our
                    technical-commercial team leverages deep product knowledge
                    to analyze your specifications and build tailored,
                    cost-effective bulk offers that align with your exact
                    operational requirements.
                  </p>
                </div>
              </div>

              {/* Engineering Engine */}
              <div className="flex flex-col justify-between rounded-2xl border border-slate-200 bg-white p-8 shadow-sm hover:shadow-md transition-shadow">
                <div>
                  <div className="inline-flex h-12 w-12 items-center justify-center rounded-xl bg-amber-50 text-amber-600 mb-6">
                    <Cpu className="h-6 w-6" />
                  </div>
                  <h3 className="text-2xl font-bold text-slate-950">
                    Engineering & Field Works
                  </h3>
                  <p className="text-sm font-medium text-amber-600 mt-1">
                    Expert Execution, Full Compliance
                  </p>
                  <p className="mt-4 text-base text-slate-600 leading-relaxed">
                    Our result-driven specialists support your projects from
                    initial design to final execution. We execute complex
                    high-and-low voltage installations, build custom control
                    panels, lay robust network cabling, install fire alarm
                    systems, and provide rigorous electrical compliance
                    monitoring and preventative maintenance.
                  </p>
                </div>
              </div>
            </div>
          </div>
        </section>

        {/* 4. TRUST PILLARS */}
        <section className="py-20 bg-white">
          <div className="mx-auto max-w-7xl px-6 lg:px-8">
            <div className="text-center max-w-3xl mx-auto mb-16">
              <h2 className="text-3xl font-bold tracking-tight text-slate-950 sm:text-4xl">
                Why Partners Choose Sogetec
              </h2>
            </div>

            <div className="grid grid-cols-1 gap-8 md:grid-cols-3">
              <div className="rounded-xl border border-slate-100 bg-slate-50 p-6">
                <div className="h-10 w-10 flex items-center justify-center rounded-lg bg-emerald-50 text-emerald-600 mb-4">
                  <ShieldCheck className="h-5 w-5" />
                </div>
                <h4 className="text-lg font-bold text-slate-950">
                  Uncompromising Quality
                </h4>
                <p className="mt-2 text-sm text-slate-600 leading-relaxed">
                  Every item in our inventory and every component we install
                  meets strict international safety and industry standards. We
                  do not cut corners on hardware.
                </p>
              </div>

              <div className="rounded-xl border border-slate-100 bg-slate-50 p-6">
                <div className="h-10 w-10 flex items-center justify-center rounded-lg bg-sky-50 text-sky-600 mb-4">
                  <Cpu className="h-5 w-5" />
                </div>
                <h4 className="text-lg font-bold text-slate-950">
                  Advanced Technical Expertise
                </h4>
                <p className="mt-2 text-sm text-slate-600 leading-relaxed">
                  Our team consists of highly qualified, certified professionals
                  who understand complex power systems, rigorous security
                  protocols, and secure field execution.
                </p>
              </div>

              <div className="rounded-xl border border-slate-100 bg-slate-50 p-6">
                <div className="h-10 w-10 flex items-center justify-center rounded-lg bg-amber-50 text-amber-600 mb-4">
                  <Clock className="h-5 w-5" />
                </div>
                <h4 className="text-lg font-bold text-slate-950">
                  Total Flexibility & Reliability
                </h4>
                <p className="mt-2 text-sm text-slate-600 leading-relaxed">
                  From emergency fault repairs to long-term industrial rollouts,
                  we adapt seamlessly to your scheduling constraints, deadlines,
                  and project budgets.
                </p>
              </div>
            </div>
          </div>
        </section>

        {/* 5. TECHNICAL SPECIALTIES & SECTORS */}
        <section className="py-20 bg-slate-950 text-white relative overflow-hidden">
          <div className="absolute inset-0 opacity-5 bg-[radial-gradient(#ffffff_1px,transparent_1px)] [background-size:20px_20px]" />
          <div className="mx-auto max-w-7xl px-6 lg:px-8 relative z-10">
            <div className="max-w-3xl mb-16">
              <h2 className="text-3xl font-bold tracking-tight sm:text-4xl">
                Technical Specialties & Sectors
              </h2>
              <p className="mt-4 text-slate-400 text-base">
                Engineered ecosystems designed for high-availability performance
                across foundational environments.
              </p>
            </div>

            <div className="grid grid-cols-1 gap-12 sm:grid-cols-2 lg:grid-cols-4">
              {/* Box 1 */}
              <div>
                <div className="flex items-center gap-2 mb-4">
                  <Building2 className="h-5 w-5 text-sky-400" />
                  <h4 className="text-lg font-bold text-white">
                    Buildings & Tertiary
                  </h4>
                </div>
                <ul className="space-y-2 text-sm text-slate-400 border-l border-slate-800 pl-4">
                  <li>Domestic & General Wiring</li>
                  <li>Fire Alarm & Detection</li>
                  <li>Electrical Compliance Audits</li>
                  <li>Low-Voltage Building Control</li>
                </ul>
              </div>

              {/* Box 2 */}
              <div>
                <div className="flex items-center gap-2 mb-4">
                  <Factory className="h-5 w-5 text-amber-400" />
                  <h4 className="text-lg font-bold text-white">
                    Industrial Networks
                  </h4>
                </div>
                <ul className="space-y-2 text-sm text-slate-400 border-l border-slate-800 pl-4">
                  <li>High-Voltage Infrastructure</li>
                  <li>Custom Control Panels</li>
                  <li>Heavy Power Cable Tracing</li>
                  <li>Substation Monitoring</li>
                </ul>
              </div>

              {/* Box 3 */}
              <div>
                <div className="flex items-center gap-2 mb-4">
                  <Lightbulb className="h-5 w-5 text-emerald-400" />
                  <h4 className="text-lg font-bold text-white">
                    Infrastructure
                  </h4>
                </div>
                <ul className="space-y-2 text-sm text-slate-400 border-l border-slate-800 pl-4">
                  <li>Street Lighting Systems</li>
                  <li>Grid Network Distribution</li>
                  <li>System Upkeep & Maintenance</li>
                  <li>Fault Detection Mapping</li>
                </ul>
              </div>

              {/* Box 4 */}
              <div>
                <div className="flex items-center gap-2 mb-4">
                  <Sun className="h-5 w-5 text-yellow-400" />
                  <h4 className="text-lg font-bold text-white">
                    Renewable Energy
                  </h4>
                </div>
                <ul className="space-y-2 text-sm text-slate-400 border-l border-slate-800 pl-4">
                  <li>Solar PV Array Integration</li>
                  <li>Alternative Power Systems</li>
                  <li>Hybrid Storage Control</li>
                  <li>Green Energy Auditing</li>
                </ul>
              </div>
            </div>
          </div>
        </section>

        {/* 6. ACTIONABLE FOOTER / CONTACT ZONE */}
        <section className="bg-white py-16 border-t border-slate-200">
          <div className="mx-auto max-w-7xl px-6 lg:px-8">
            <div className="relative isolate overflow-hidden bg-slate-900 px-6 py-12 shadow-2xl rounded-3xl sm:px-12 md:py-16 lg:flex lg:items-center lg:gap-x-20 lg:px-20">
              <div className="absolute inset-0 -z-10 bg-[radial-gradient(dark_blue_circle_at_top_right,rgba(14,165,233,0.15),transparent)]" />

              <div className="w-full lg:flex-auto">
                <h2 className="text-2xl font-bold tracking-tight text-white sm:text-3xl">
                  Ready to power your next project or source bulk electrical
                  components?
                </h2>
                <p className="mt-4 text-base leading-6 text-slate-300">
                  Get in touch with our technical commercial team in Douala
                  today for custom quotes and project tenders.
                </p>

                <div className="mt-6 grid grid-cols-1 gap-4 sm:grid-cols-2 text-sm text-slate-300 border-t border-slate-800 pt-6">
                  <div className="flex items-start gap-2.5">
                    <MapPin className="h-5 w-5 text-sky-400 shrink-0 mt-0.5" />
                    <span>
                      Bessengue, Cinema Eden, Douala, Littoral
                      <br />
                      B.P. 11058, CM
                    </span>
                  </div>
                  <div className="flex items-center gap-2.5">
                    <Phone className="h-5 w-5 text-amber-400 shrink-0" />
                    <a
                      href="tel:+237696139122"
                      className="hover:text-white transition-colors"
                    >
                      +237 696 139 122
                    </a>
                  </div>
                </div>
              </div>

              <div className="mt-10 flex shrink-0 lg:mt-0 lg:flex-auto justify-start lg:justify-end">
                <a
                  href="tel:+237696139122"
                  className="inline-flex items-center gap-2 rounded-xl bg-sky-500 px-5 py-3 text-sm font-semibold text-white shadow-sm hover:bg-sky-400 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-sky-500 transition-all group"
                >
                  Request Technical Offer
                  <ArrowUpRight className="h-4 w-4 transform group-hover:translate-x-0.5 group-hover:-translate-y-0.5 transition-transform" />
                </a>
              </div>
            </div>
          </div>
        </section>
      </div>
    </Container>
  );
}
