import {
  Alert,
  Box,
  Button,
  Grid,
  Paper,
  TextField,
  Typography,
} from "@mui/material";
import { CircleArrowLeft } from "lucide-react";
import { useSnackbar } from "notistack";
import { useEffect, useState } from "react";
import { useNavigate, useSearchParams } from "react-router-dom";
import LoadingSpinner from "../components/Spinner/LoadingSpinner";
import type { BrandData } from "../data/BrandData";
import api from "../lib/axios";
import { API_PREFIX } from "../lib/Constant";
import { ERROR_MESSAGES } from "../lib/ErrorMessages";
import { isNullOrEmpty } from "../utils/StringExtensions";

export default function BrandEditPage() {
  const navigate = useNavigate();
  const { enqueueSnackbar } = useSnackbar();
  const [searchParams] = useSearchParams();
  const [loading, setLoading] = useState<boolean>(false);
  const [saving, setSaving] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);
  const ID = searchParams.get("id");
  const isEditMode = Boolean(ID);

  const [formData, setFormData] = useState({
    name: "",
    logo: "",
  });

  useEffect(() => {
    if (!isEditMode) return;

    const fetchBrandData = async () => {
      setLoading(true);
      setError(null);
      try {
        const response = await api.get<BrandData>(`${API_PREFIX}/brands/${ID}`);
        setFormData({
          name: response.data.name || "",
          logo: response.data.logoUrl || "",
        });
      } catch (err: any) {
        setError("Failed to populate brand record data.");
      } finally {
        setLoading(false);
      }
    };

    fetchBrandData();
  }, [isEditMode]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
  };

  const handleSubmit = async (e: any) => {
    e.preventDefault();

    if (isNullOrEmpty(formData.name)) {
      setError("Name required");
      return;
    }

    setSaving(true);
    setError(null);
    try {
      if (isEditMode) {
        await api.put(`${API_PREFIX}/brands/${ID}`, {
          id: ID,
          name: formData.name,
          imageUrl: formData.logo,
        });
      } else {
        await api.post(`${API_PREFIX}/brands`, {
          name: formData.name,
          imageUrl: formData.logo,
        });
      }

      enqueueSnackbar(
        `Brand "${formData.name} was ${isEditMode ? "updated" : "created"}.`,
        {
          variant: "success",
          onExit: () => {
            navigate(-1);
          },
        },
      );
    } catch (err: any) {
      enqueueSnackbar("An error occurred while saving the entity changes.", {
        variant: "error",
      });
      setError(
        ERROR_MESSAGES[err.details.title] ||
          "An error occurred while saving the changes.",
      );
    } finally {
      setSaving(false);
    }
  };

  return (
    <Box>
      <LoadingSpinner isLoading={loading} />
      <Box>
        <span>{`${isEditMode ? "Edit" : "Add"}`} a new Brand</span>
        <Button variant="text" onClick={() => navigate(-1)}>
          <CircleArrowLeft />
          Back to Brand List
        </Button>
        {error && (
          <Alert severity="error" sx={{ mb: 3, mt: 2 }}>
            {error}
          </Alert>
        )}
      </Box>
      <Paper
        sx={{
          padding: "50px",
          display: "flex",
          justifyContent: "center",
        }}
        elevation={1}
      >
        <div>
          <Box
            component="form"
            onSubmit={handleSubmit}
            sx={{ maxWidth: 600, width: "100%", mx: "auto", mt: 4 }}
          >
            <Grid container spacing={3} sx={{ alignItems: "center" }}>
              <Grid size={{ xs: 3 }}>
                <Typography
                  variant="body1"
                  component="label"
                  htmlFor="brand-name"
                  sx={{ display: "block", textAlign: "right", fontWeight: 500 }}
                >
                  Name:
                </Typography>
              </Grid>

              <Grid size={{ xs: 9 }}>
                <TextField
                  id="brand-name"
                  name="name"
                  value={formData.name}
                  onChange={handleChange}
                  variant="outlined"
                  size="small"
                  fullWidth
                  placeholder="Enter brand name"
                  disabled={saving}
                />
              </Grid>

              <Grid size={{ xs: 3 }}>
                <Typography
                  variant="body1"
                  component="label"
                  htmlFor="brand-logo"
                  sx={{ display: "block", textAlign: "right", fontWeight: 500 }}
                >
                  Logo:
                </Typography>
              </Grid>

              <Grid size={{ xs: 9 }}>
                <TextField
                  id="brand-logo"
                  name="logo"
                  value={formData.logo}
                  onChange={handleChange}
                  variant="outlined"
                  size="small"
                  fullWidth
                  placeholder="Enter logo image URL or asset key"
                  disabled={saving}
                />
              </Grid>

              <Grid
                size={{ xs: 12 }}
                sx={{ display: "flex", justifyContent: "flex-end" }}
              >
                <Button
                  type="submit"
                  variant="contained"
                  sx={{ backgroundColor: "var(--color-green)" }}
                  loading={saving}
                  loadingPosition="start"
                >
                  Save Brand
                </Button>
              </Grid>
            </Grid>
          </Box>
        </div>
      </Paper>
    </Box>
  );
}
