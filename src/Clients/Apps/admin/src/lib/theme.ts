export const applyTheme = (theme: any) => {
  if (!theme) return;

  const root = document.documentElement;

  if (theme.primaryColor) {
    root.style.setProperty("--primary-color", theme.primaryColor);
  }

  if (theme.mode === "dark") {
    root.classList.add("dark");
  } else {
    root.classList.remove("dark");
  }
};
