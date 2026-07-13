import tailwindcss from "@tailwindcss/vite";
import basicSsl from "@vitejs/plugin-basic-ssl";
import react from "@vitejs/plugin-react";
import { defineConfig } from "vite";
import mkcert from "vite-plugin-mkcert";

// https://vite.dev/config/
export default defineConfig({
  plugins: [react(), mkcert(), basicSsl(), tailwindcss()],
  server: {
    allowedHosts: [".sogetecsarl.com"],
  },
  optimizeDeps: {
    include: ["keycloak-js"],
  },
});
