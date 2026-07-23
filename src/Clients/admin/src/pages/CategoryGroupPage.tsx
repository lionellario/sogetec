import {
  Alert,
  Box,
  Button,
  ButtonGroup,
  ListItemIcon,
  MenuItem,
  lighten,
} from "@mui/material";
import { Pencil, ShieldCheck, ShieldOff, Trash2 } from "lucide-react";
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
import type { CategoryGroupData } from "../data/CategoryGroupData";
import { API_PREFIX } from "../lib/Constant";
import { ERROR_MESSAGES } from "../lib/ErrorMessages";
import api from "../lib/axios";

const CategoryGroupPage = () => {
  const navigate = useNavigate();
  const [deleting, setIsDeleting] = useState<boolean>(false);
  const [isLoading, setIsLoading] = useState<boolean>(true);
  const [data, setGroups] = useState<CategoryGroupData[]>([]);
  const [error, setError] = useState<string | string[] | null>(null);

  useEffect(() => {
    async function loadGroups() {
      try {
        const response = await api.get(
          `${API_PREFIX}/category-groups?includeInactive=true`,
        );
        setGroups(response.data);
      } catch (err) {
        setError("An error occurred while loading brands.");
      } finally {
        setIsLoading(false);
      }
    }

    loadGroups();
  }, []);

  const deleteGroups = async (
    ids: Set<string>,
    table?: MRT_TableInstance<CategoryGroupData> | null,
  ) => {
    setIsDeleting(true);

    try {
      await api.delete(`${API_PREFIX}/category-groups`, {
        data: {
          ids: Array.from(ids),
        },
      });
      enqueueSnackbar("Category groups were successfully deleted", {
        variant: "success",
      });
      setGroups((prevBrands) => prevBrands.filter((d) => !ids.has(d.id)));
      setError(null);
      table?.resetRowSelection();
    } catch (err: any) {
      setError(
        ERROR_MESSAGES[err.details.title] ||
          "An error occurred while saving the changes.",
      );
    } finally {
      setIsDeleting(false);
    }
  };

  const changeStatus = async (
    ids: Set<string>,
    isActive: boolean,
    table?: MRT_TableInstance<CategoryGroupData> | null,
  ) => {
    setIsLoading(true);

    try {
      await api.put(`${API_PREFIX}/category-groups/change-status`, {
        ids: Array.from(ids),
        isActive: isActive,
      });
      enqueueSnackbar("Category groups were successfully updated", {
        variant: "success",
      });
      setGroups((prevGroups) =>
        prevGroups.map((group) =>
          ids.has(group.id) ? { ...group, isActive: isActive } : group,
        ),
      );
      setError(null);
      table?.resetRowSelection();
    } catch (err: any) {
      setError(
        ERROR_MESSAGES[err.details.title] ||
          "An error occurred while saving the changes.",
      );
    } finally {
      setIsLoading(false);
    }
  };

  const columns = useMemo<MRT_ColumnDef<CategoryGroupData>[]>(
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
        accessorFn: (row) => `${row.isActive}`,
        id: "isActive",
        header: "Status",
        size: 250,
        Cell: ({ row }) => (
          <span>
            {row.original.isActive ? (
              <ShieldCheck color="green" />
            ) : (
              <ShieldOff color="red" />
            )}
          </span>
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
    id: "category-group-table",
    selectAllMode: "page",
    enableRowActions: true,
    enableRowSelection: true,
    initialState: {
      showColumnFilters: false,
      showGlobalFilter: true,
      columnPinning: {
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
          const status: boolean = !row.original.isActive;
          changeStatus(ids, status);
          closeMenu();
        }}
        sx={{ m: 0 }}
      >
        <ListItemIcon>
          {row.original.isActive ? (
            <ShieldOff color="orange" />
          ) : (
            <ShieldCheck color="green" />
          )}
        </ListItemIcon>
        {row.original.isActive ? "Unpublish" : "Publish"}
      </MenuItem>,
      <MenuItem
        key={2}
        onClick={() => {
          const ids: Set<string> = new Set([row.original.id]);
          deleteGroups(ids);
          closeMenu();
        }}
        sx={{ m: 0 }}
      >
        <ListItemIcon>
          <Trash2 color="red" />
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

        deleteGroups(ids, table);
      };

      const handleStatusChange = async (status: boolean) => {
        const ids: Set<string> = new Set();
        table.getSelectedRowModel().flatRows.map((row) => {
          ids.add(row.original.id);
        });

        changeStatus(ids, status, table);
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
              <Button
                variant="contained"
                sx={{ backgroundColor: "var(--color-green)" }}
                onClick={() => navigate("create")}
                disableElevation
              >
                Add
              </Button>
              <ButtonGroup
                variant="contained"
                aria-label="Publish or Unpublish"
                color="warning"
                disabled={table.getSelectedRowModel().rows.length == 0}
                disableElevation
              >
                <Button onClick={() => handleStatusChange(true)}>
                  Publish
                </Button>
                <Button onClick={() => handleStatusChange(false)}>
                  Unpublish
                </Button>
              </ButtonGroup>
              <Button
                color="error"
                disabled={table.getSelectedRowModel().rows.length == 0}
                onClick={handleDelete}
                variant="contained"
                disableElevation
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
      <MaterialReactTable table={table} />
    </div>
  );
};

export default CategoryGroupPage;
