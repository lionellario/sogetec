import "./LoadingSpinner.css";

export default function LoadingSpinner({ isLoading }: { isLoading: boolean }) {
  if (!isLoading) return null;

  return (
    <div className="spinner-backdrop">
      <div className="spinner-circle"></div>
    </div>
  );
}
