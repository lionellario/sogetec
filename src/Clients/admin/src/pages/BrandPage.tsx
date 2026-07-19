import {
  Alert,
  Box,
  Button,
  ListItemIcon,
  MenuItem,
  lighten,
} from "@mui/material";
import { Pencil, Trash2 } from "lucide-react";
import {
  MRT_GlobalFilterTextField,
  MRT_ToggleFiltersButton,
  MaterialReactTable,
  useMaterialReactTable,
  type MRT_ColumnDef,
  type MRT_TableInstance,
} from "material-react-table";
import { enqueueSnackbar } from "notistack";
import { useEffect, useMemo, useState } from "react";
import { useNavigate } from "react-router-dom";
import LoadingSpinner from "../components/Spinner/LoadingSpinner";
import type { BrandData } from "../data/BrandData";
import { API_PREFIX } from "../lib/Constant";
import { ERROR_MESSAGES } from "../lib/ErrorMessages";
import api from "../lib/axios";

const BrandPage = () => {
  const navigate = useNavigate();
  const [deleting, setDeleting] = useState<boolean>(false);
  const [isLoading, setLoading] = useState<boolean>(true);
  const [data, setProducts] = useState<BrandData[]>([]);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    async function loadProducts() {
      try {
        const response = await api.get(`${API_PREFIX}/brands`);
        setProducts(response.data);
      } catch (err) {
        setError("An error occurred while loading brands.");
      } finally {
        setLoading(false);
      }
    }

    loadProducts();
  }, []);

  const deleteProducts = async (
    ids: Set<string>,
    table?: MRT_TableInstance<BrandData> | null,
  ) => {
    setDeleting(true);

    try {
      await api.delete(`${API_PREFIX}/brands`, {
        data: {
          ids: Array.from(ids),
        },
      });
      enqueueSnackbar("Brands were successfully deleted", {
        variant: "success",
      });
      setProducts((prevBrands) => prevBrands.filter((d) => !ids.has(d.id)));
      setError(null);
      table?.resetRowSelection();
    } catch (err: any) {
      setError(
        ERROR_MESSAGES[err.details.title] ||
          "An error occurred while saving the changes.",
      );
    } finally {
      setDeleting(false);
    }
  };

  const columns = useMemo<MRT_ColumnDef<BrandData>[]>(
    () => [
      {
        accessorFn: (row) => `${row.name}`,
        id: "name",
        header: "Name",
        size: 250,
        Cell: ({ renderedCellValue, row }) => (
          <Box
            sx={{
              display: "flex",
              alignItems: "center",
              gap: "1rem",
            }}
          >
            <img
              alt={row.original.name}
              height={30}
              width={30}
              src="https://picsum.photos/200/300?random=1"
              loading="lazy"
              style={{ borderRadius: "50%" }}
            />
            <span>{renderedCellValue}</span>
          </Box>
        ),
      },
      {
        accessorFn: (row) => new Date(row.createdAt),
        id: "createdAt",
        header: "Created At",
        filterVariant: "date",
        filterFn: "lessThan",
        sortingFn: "datetime",
        Cell: ({ cell }) => cell.getValue<Date>()?.toLocaleDateString(),
        Header: ({ column }) => <em>{column.columnDef.header}</em>,
        muiFilterTextFieldProps: {
          sx: {
            minWidth: "250px",
          },
        },
      },
      {
        accessorFn: (row) => new Date(row.lastModifiedAt),
        id: "lastModifiedAt",
        header: "Last Modified At",
        filterVariant: "date",
        filterFn: "lessThan",
        sortingFn: "datetime",
        Cell: ({ cell }) => cell.getValue<Date>()?.toLocaleDateString(),
        Header: ({ column }) => <em>{column.columnDef.header}</em>,
        muiFilterTextFieldProps: {
          sx: {
            minWidth: "250px",
          },
        },
      },
    ],
    [],
  );

  const table = useMaterialReactTable({
    columns,
    data,
    enableRowActions: true,
    enableRowSelection: true,
    initialState: {
      showColumnFilters: false,
      showGlobalFilter: true,
      columnPinning: {
        left: ["mrt-row-expand", "mrt-row-select"],
        right: ["mrt-row-actions"],
      },
    },
    paginationDisplayMode: "pages",
    positionToolbarAlertBanner: "bottom",
    muiSearchTextFieldProps: {
      size: "small",
      variant: "outlined",
    },
    muiPaginationProps: {
      color: "secondary",
      rowsPerPageOptions: [10, 20, 30],
      shape: "rounded",
      variant: "outlined",
    },
    renderRowActionMenuItems: ({ closeMenu, row }) => [
      <MenuItem
        key={0}
        onClick={() => {
          navigate(`edit?id=${row.original.id}`);
          closeMenu();
        }}
        sx={{ m: 0 }}
      >
        <ListItemIcon>
          <Pencil />
        </ListItemIcon>
        Edit
      </MenuItem>,
      <MenuItem
        key={1}
        onClick={() => {
          const ids: Set<string> = new Set([row.original.id]);
          deleteProducts(ids);
          closeMenu();
        }}
        sx={{ m: 0 }}
      >
        <ListItemIcon>
          <Trash2 />
        </ListItemIcon>
        Delete
      </MenuItem>,
    ],
    renderTopToolbar: ({ table }) => {
      const handleDelete = async () => {
        const ids: Set<string> = new Set();
        table.getSelectedRowModel().flatRows.map((row) => {
          ids.add(row.original.id);
        });
        deleteProducts(ids, table);
      };

      return (
        <Box
          sx={(theme) => ({
            backgroundColor: lighten(theme.palette.background.default, 0.05),
            display: "flex",
            gap: "0.5rem",
            p: "8px",
            justifyContent: "space-between",
          })}
        >
          <Box sx={{ display: "flex", gap: "0.5rem", alignItems: "center" }}>
            <MRT_GlobalFilterTextField table={table} />
            <MRT_ToggleFiltersButton table={table} />
          </Box>
          <Box>
            <Box sx={{ display: "flex", gap: "0.5rem" }}>
              <Button variant="contained" onClick={() => navigate("create")}>
                Add
              </Button>
              <Button
                color="error"
                disabled={!table.getIsSomeRowsSelected()}
                onClick={handleDelete}
                variant="contained"
              >
                Delete
              </Button>
            </Box>
          </Box>
        </Box>
      );
    },
  });

  return (
    <div>
      <LoadingSpinner isLoading={deleting || isLoading} />
      {error && (
        <Alert
          severity="error"
          sx={{ mb: 3, mt: 2 }}
          onClose={() => setError(null)}
        >
          {error}
        </Alert>
      )}
      <MaterialReactTable table={table} />;
    </div>
  );
};

export default BrandPage;
