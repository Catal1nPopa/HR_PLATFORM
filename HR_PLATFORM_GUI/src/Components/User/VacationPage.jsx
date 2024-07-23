// VacationPage.js
import { useEffect, useMemo, useState } from "react";
import {
  MaterialReactTable,
  useMaterialReactTable,
} from "material-react-table";

const VacationPage = () => {
  //data and fetching state
  const [data, setData] = useState([]);
  const [isError, setIsError] = useState(false);
  const [isLoading, setIsLoading] = useState(false);
  const [isRefetching, setIsRefetching] = useState(false);
  const [rowCount, setRowCount] = useState(0);

  //table state
  const [columnFilters, setColumnFilters] = useState([]);
  const [globalFilter, setGlobalFilter] = useState("");
  const [sorting, setSorting] = useState([]);
  const [pagination, setPagination] = useState({
    pageIndex: 0,
    pageSize: 10,
  });

  useEffect(() => {
    const fetchData = async () => {
      if (!data.length) {
        setIsLoading(true);
      } else {
        setIsRefetching(true);
      }

      const url = new URL(
        "/api/Vacation/GetVacationsEmployees",
        process.env.NODE_ENV === "production"
          ? "https://www.material-react-table.com"
          : "http://localhost:3000"
      );

      url.searchParams.set(
        "start",
        `${pagination.pageIndex * pagination.pageSize}`
      );
      url.searchParams.set("size", `${pagination.pageSize}`);
      url.searchParams.set("filters", JSON.stringify(columnFilters ?? []));
      url.searchParams.set("globalFilter", globalFilter ?? "");
      url.searchParams.set("sorting", JSON.stringify(sorting ?? []));

      try {
        const response = await fetch(url.href);
        const json = await response.json();
        setData(json);
        setRowCount(json.length); // Assuming the response has a length property
      } catch (error) {
        setIsError(true);
        console.error(error);
        return;
      }
      setIsError(false);
      setIsLoading(false);
      setIsRefetching(false);
    };
    fetchData();
  }, [
    columnFilters,
    globalFilter,
    pagination.pageIndex,
    pagination.pageSize,
    sorting,
  ]);

  const columns = useMemo(
    () => [
      {
        accessorKey: "id",
        header: "ID",
      },
      {
        accessorKey: "codEmployee",
        header: "Employee Code",
      },
      {
        accessorKey: "startDate",
        header: "Start Date",
      },
      {
        accessorKey: "endDate",
        header: "End Date",
      },
      {
        accessorKey: "daysVacation",
        header: "Days Vacation",
      },
      {
        accessorKey: "vacationDaysLeft",
        header: "Vacation Days Left",
      },
      {
        accessorKey: "typeVacation",
        header: "Type of Vacation",
      },
    ],
    []
  );

  const table = useMaterialReactTable({
    columns,
    data,
    enableRowSelection: true,
    getRowId: (row) => row.id,
    initialState: { showColumnFilters: true },
    manualFiltering: true,
    manualPagination: true,
    manualSorting: true,
    muiToolbarAlertBannerProps: isError
      ? {
          color: "error",
          children: "Error loading data",
        }
      : undefined,
    onColumnFiltersChange: setColumnFilters,
    onGlobalFilterChange: setGlobalFilter,
    onPaginationChange: setPagination,
    onSortingChange: setSorting,
    rowCount,
    state: {
      columnFilters,
      globalFilter,
      isLoading,
      pagination,
      showAlertBanner: isError,
      showProgressBars: isRefetching,
      sorting,
    },
  });

  return <MaterialReactTable table={table} />;
};

export default VacationPage;
