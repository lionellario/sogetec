import { useLocation } from "react-router-dom";

export function useIsHomePage() {
  const location = useLocation();
  return location.pathname === "/";
}
