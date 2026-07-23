import {
  Box,
  Button,
  Checkbox,
  Grid,
  Paper,
  TextField,
  Typography,
} from "@mui/material";
import { CircleArrowLeft } from "lucide-react";
import { useSnackbar } from "notistack";
import { useEffect, useState } from "react";
import { useNavigate, useSearchParams } from "react-router-dom";
import { ErrorBox } from "../components/ErrorBox";
import LoadingSpinner from "../components/Spinner/LoadingSpinner";
import type { CategoryGroupData } from "../data/CategoryGroupData";
import api from "../lib/axios";
import { API_PREFIX } from "../lib/Constant";
import { ERROR_MESSAGES } from "../lib/ErrorMessages";
import { isNullOrEmpty } from "../utils/StringExtensions";

export default function CategoryGroupEditPage() {
  const navigate = useNavigate();
  const { enqueueSnackbar } = useSnackbar();
  const [searchParams] = useSearchParams();
  const [loading, setLoading] = useState<boolean>(false);
  const [saving, setSaving] = useState<boolean>(false);
  const [errors, setErrors] = useState<string | string[] | null>(null);

  const ID = searchParams.get("id");
  const isEditMode = Boolean(ID);

  const [formData, setFormData] = useState({
    id: "",
    name: "",
    nameFr: "",
    imageUrl: "",
    isActive: false,
    sortOrder: 0,
  });

  useEffect(() => {
    if (!isEditMode) return;

    const fetchCategroupData = async () => {
      setLoading(true);
      setErrors(null);
      try {
        const response = await api.get<CategoryGroupData>(
          `${API_PREFIX}/category-groups/${ID}`,
        );
        setFormData({
          id: response.data.id,
          name: response.data.name || "",
          nameFr: response.data.nameFr || "",
          imageUrl: response.data.imageUrl || "",
          isActive: response.data.isActive || false,
          sortOrder: response.data.sortOrder || 0,
        });
      } catch (err: any) {
        setErrors(["Failed to populate category group record data."]);
      } finally {
        setLoading(false);
      }
    };

    fetchCategroupData();
  }, [isEditMode]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
  };

  const handleSubmit = async (e: any) => {
    e.preventDefault();
    const errs: string[] = [];

    if (isNullOrEmpty(formData.name)) {
      errs.push("Name required");
    }

    if (isNullOrEmpty(formData.nameFr)) {
      errs.push("Le nom en français dois être rempli.");
    }

    if (isNullOrEmpty(formData.imageUrl)) {
      errs.push("Image is required.");
    }

    if (errs.length > 0) {
      setErrors(errs);
      return;
    }

    setSaving(true);
    setErrors(null);
    try {
      if (isEditMode) {
        await api.put(`${API_PREFIX}/category-groups/${ID}`, {
          id: ID,
          name: formData.name,
          nameFr: formData.nameFr,
          imageUrl: formData.imageUrl,
          isActive: formData.isActive,
          sortOrder: formData.sortOrder,
        });
      } else {
        await api.post(`${API_PREFIX}/category-groups`, {
          name: formData.name,
          nameFr: formData.nameFr,
          imageUrl: formData.imageUrl,
          isActive: formData.isActive,
          sortOrder: formData.sortOrder,
        });
      }

      enqueueSnackbar(
        `Category Group "${formData.name} was ${isEditMode ? "updated" : "created"}.`,
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
      setErrors(
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
        <span>{`${isEditMode ? "Edit" : "Add"}`} a new Category Group</span>
        <Button variant="text" onClick={() => navigate(-1)}>
          <CircleArrowLeft />
          Back to Group List
        </Button>
        <ErrorBox errors={errors} />
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
                  htmlFor="name"
                  sx={{ display: "block", textAlign: "right", fontWeight: 500 }}
                >
                  Name:
                </Typography>
              </Grid>

              <Grid size={{ xs: 9 }}>
                <TextField
                  id="name"
                  name="name"
                  value={formData.name}
                  onChange={handleChange}
                  variant="outlined"
                  size="small"
                  fullWidth
                  placeholder="Enter name"
                  disabled={saving}
                />
              </Grid>

              <Grid size={{ xs: 3 }}>
                <Typography
                  variant="body1"
                  component="label"
                  htmlFor="nameFr"
                  sx={{ display: "block", textAlign: "right", fontWeight: 500 }}
                >
                  Nom en Français:
                </Typography>
              </Grid>

              <Grid size={{ xs: 9 }}>
                <TextField
                  id="nameFr"
                  name="nameFr"
                  value={formData.nameFr}
                  onChange={handleChange}
                  variant="outlined"
                  size="small"
                  fullWidth
                  placeholder="Entre le nom en français"
                  disabled={saving}
                />
              </Grid>

              <Grid size={{ xs: 3 }}>
                <Typography
                  variant="body1"
                  component="label"
                  htmlFor="image"
                  sx={{ display: "block", textAlign: "right", fontWeight: 500 }}
                >
                  Image Url:
                </Typography>
              </Grid>

              <Grid size={{ xs: 9 }}>
                <TextField
                  id="image"
                  name="imageUrl"
                  value={formData.imageUrl}
                  onChange={handleChange}
                  variant="outlined"
                  size="small"
                  fullWidth
                  placeholder="Enter logo image URL or asset key"
                  disabled={saving}
                />
              </Grid>

              <Grid size={{ xs: 3 }}>
                <Typography
                  variant="body1"
                  component="label"
                  htmlFor="status"
                  sx={{ display: "block", textAlign: "right", fontWeight: 500 }}
                >
                  Publish:
                </Typography>
              </Grid>

              <Grid size={{ xs: 9 }}>
                <Checkbox
                  checked={formData.isActive}
                  onChange={(event: React.ChangeEvent<HTMLInputElement>) =>
                    setFormData((prev) => ({
                      ...prev,
                      isActive: event.target.checked,
                    }))
                  }
                  slotProps={{
                    input: { "aria-label": "controlled" },
                  }}
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
                  disableElevation
                >
                  Save Group
                </Button>
              </Grid>
            </Grid>
          </Box>
        </div>
      </Paper>
    </Box>
  );
}
