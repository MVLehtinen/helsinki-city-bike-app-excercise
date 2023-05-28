import {
  Paper,
  Table,
  TableContainer,
  TableCell,
  TableHead,
  TableRow,
  Typography,
  TableBody,
  TablePagination,
  TableFooter,
  TableSortLabel,
} from "@mui/material";
import { useEffect, useState } from "react";
import { Link } from "react-router-dom";

function Journeys() {
  const [journeys, setJourneys] = useState(null);
  const [total, setTotal] = useState(0);
  const [page, setPage] = useState(1);
  const [pageSize, setPageSize] = useState(10);
  const [orderBy, setOrderBy] = useState("");
  const [order, setOrder] = useState("asc");

  function constructQueryString() {
    let query = `?page=${page}&pageSize=${pageSize}`;
    if (orderBy !== "") {
      query += `&orderBy=${orderBy}${
        order === "asc" ? "Ascending" : "Descending"
      }`;
    }
    return query;
  }

  async function getJourneys() {
    const queryString = constructQueryString();
    const response = await fetch(
      "http://localhost:5000/api/journeys" + queryString
    );
    const data = await response.json();
    setJourneys(data.result);
    setTotal(data.total);
  }

  function getPrettyDateString(dateString) {
    const date = new Date(dateString);
    return date.toLocaleString();
  }

  function handlePageChange(e, newPage) {
    setPage(newPage + 1);
  }

  function handleRowsPerPageChange(e) {
    setPageSize(parseInt(e.target.value));
    setPage(1);
  }

  useEffect(() => {
    getJourneys();
  }, [page, pageSize, orderBy, order]);

  const SortLabel = (props) => (
    <TableSortLabel
      active={orderBy === props.sortBy}
      direction={orderBy === props.sortBy ? order : "asc"}
      onClick={() => {
        const isAsc = orderBy === props.sortBy && order === "asc";
        setOrder(isAsc ? "desc" : "asc");
        setOrderBy(props.sortBy);
      }}
    >
      {props.children}
    </TableSortLabel>
  );

  return (
    <>
      <Typography variant="h4" sx={{ mb: 5, mt: 5 }}>
        Journeys
      </Typography>
      <TableContainer component={Paper}>
        <Table size="small">
          <TableHead>
            <TableRow>
              <TableCell>
                <SortLabel sortBy="departure">Departure time</SortLabel>
              </TableCell>
              <TableCell>
                <SortLabel sortBy="return">Return time</SortLabel>
              </TableCell>
              <TableCell>
                <SortLabel sortBy="departureStation">
                  Departure station
                </SortLabel>
              </TableCell>
              <TableCell>
                <SortLabel sortBy="returnStation">Return station</SortLabel>
              </TableCell>
              <TableCell>
                <SortLabel sortBy="distance">Covered distance</SortLabel>
              </TableCell>
              <TableCell>
                <SortLabel sortBy="duration">Duration</SortLabel>
              </TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {journeys
              ? journeys.map((j) => (
                  <TableRow key={j.id}>
                    <TableCell>{getPrettyDateString(j.departure)}</TableCell>
                    <TableCell>{getPrettyDateString(j.return)}</TableCell>
                    <TableCell>
                      <Link
                        style={{ textDecoration: "none" }}
                        to={`/stations/${j.departureStationId}`}
                      >
                        {j.departureStationName}
                      </Link>
                    </TableCell>
                    <TableCell>
                      <Link
                        style={{ textDecoration: "none" }}
                        to={`/stations/${j.returnStationId}`}
                      >
                        {j.returnStationName}
                      </Link>
                    </TableCell>
                    <TableCell>{j.coveredDistance} m</TableCell>
                    <TableCell>{j.duration}</TableCell>
                  </TableRow>
                ))
              : null}
          </TableBody>
          <TableFooter>
            <TableRow>
              <TablePagination
                page={page - 1}
                rowsPerPage={pageSize}
                rowsPerPageOptions={[10, 50, 100]}
                count={total}
                onPageChange={handlePageChange}
                onRowsPerPageChange={handleRowsPerPageChange}
              />
            </TableRow>
          </TableFooter>
        </Table>
      </TableContainer>
    </>
  );
}

export default Journeys;
