import { useEffect, useMemo, useState } from "react";

//MRT Imports
import {
  MaterialReactTable,
  useMaterialReactTable,
  type MRT_ColumnDef,
  MRT_GlobalFilterTextField,
  MRT_ToggleFiltersButton,
} from "material-react-table";
import {
  Alert,
  Box,
  Button,
  ListItemIcon,
  MenuItem,
  lighten,
} from "@mui/material";

import { AccountCircle, Send } from "@mui/icons-material";
import type { BrandData } from "../data/BrandData";
import { useNavigate } from "react-router-dom";
import { API_PREFIX } from "../lib/Constant";
import api from "../lib/axios";
import LoadingSpinner from "../components/Spinner/LoadingSpinner";
import { enqueueSnackbar } from "notistack";
import { ERROR_MESSAGES } from "../lib/ErrorMessages";

const BrandPage = () => {
  const navigate = useNavigate();
  const [deleting, setDeleting] = useState<boolean>(false);
  const [data, setProducts] = useState<BrandData[]>([]);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    async function loadProducts() {
      const response = await api.get(`${API_PREFIX}/brands`);
      setProducts(response.data);
    }

    loadProducts();
  }, []);

  const columns = useMemo<MRT_ColumnDef<BrandData>[]>(
    () => [
      {
        accessorFn: (row) => `${row.name}`, //accessorFn used to join multiple data into a single cell
        id: "name", //id is still required when using accessorFn instead of accessorKey
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
              alt="brand logo"
              height={30}
              width={30}
              src="https://picsum.photos/200/300?random=1"
              loading="lazy"
              style={{ borderRadius: "50%" }}
            />
            {/* using renderedCellValue instead of cell.getValue() preserves filter match highlighting */}
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
    data, //data must be memoized or stable (useState, useMemo, defined outside of this component, etc.)
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
          <AccountCircle />
        </ListItemIcon>
        Edit
      </MenuItem>,
      <MenuItem
        key={1}
        onClick={() => {
          // Send email logic...
          closeMenu();
        }}
        sx={{ m: 0 }}
      >
        <ListItemIcon>
          <Send />
        </ListItemIcon>
        Delete
      </MenuItem>,
    ],
    renderTopToolbar: ({ table }) => {
      const handleDelete = async () => {
        setDeleting(true);
        const ids: number[] = [];
        table.getSelectedRowModel().flatRows.map((row) => {
          ids.push(row.original.id);
        });

        try {
          await api.delete(`${API_PREFIX}/brands`, {
            data: {
              ids: ids,
            },
          });
        } catch (err: any) {
          setError(
            ERROR_MESSAGES[err.details.title] ||
              "An error occurred while saving the changes.",
          );
        } finally {
          setDeleting(false);
        }
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
            {/* import MRT sub-components */}
            <MRT_GlobalFilterTextField table={table} />
            <MRT_ToggleFiltersButton table={table} />
          </Box>
          <Box>
            <Box sx={{ display: "flex", gap: "0.5rem" }}>
              <Button variant="contained" onClick={() => navigate("create")}>
                Add
              </Button>
              <Button
                color="success"
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
      <LoadingSpinner isLoading={deleting} />
      {error && (
        <Alert severity="error" sx={{ mb: 3, mt: 2 }}>
          {error}
        </Alert>
      )}
      <MaterialReactTable table={table} />;
    </div>
  );
};

export default BrandPage;
