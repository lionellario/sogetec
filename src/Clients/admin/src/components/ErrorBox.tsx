import { Alert, type SxProps, type Theme } from "@mui/material";
import { Dot } from "lucide-react";

export interface ErrorProps {
  errors?: string | string[] | null;
  sx?: SxProps<Theme> | undefined;
}
export const ErrorBox = (props: ErrorProps) => {
  const defaultSX: SxProps<Theme> | undefined = { mb: 3, mt: 2 };

  if (!props.errors) {
    return null;
  }

  const errors: string[] =
    typeof props.errors == "string" ? [props.errors] : props.errors;

  return (
    <Alert severity="error" sx={props.sx ?? defaultSX}>
      {errors.length < 2 ? (
        errors[0]
      ) : (
        <ul>
          {errors.map((item) => (
            <li key={item} style={{ display: "flex", gap: "5px" }}>
              <Dot />
              <span>{item}</span>
            </li>
          ))}
        </ul>
      )}
    </Alert>
  );
};
