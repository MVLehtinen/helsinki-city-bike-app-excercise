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
  TextField,
  Button,
  FormControl,
  InputLabel,
  Select,
  Grid,
  MenuItem,
  LinearProgress,
  Box,
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
  const [search, setSearch] = useState("");
  const [month, setMonth] = useState(0);
  const [loading, setLoading] = useState(true);

  function constructQueryString() {
    let query = `?page=${page}&pageSize=${pageSize}`;
    if (orderBy !== "") {
      query += `&orderBy=${orderBy}${
        order === "asc" ? "Ascending" : "Descending"
      }`;
    }
    if (search !== null && search.trim() !== "") query += `&search=${search}`;
    if (month !== 0) query += `&month=${month}`;
    return query;
  }

  async function getJourneys() {
    setLoading(true);
    const queryString = constructQueryString();
    const response = await fetch(
      "http://localhost:5000/api/journeys" + queryString
    );
    const data = await response.json();
    setJourneys(data.result);
    setTotal(data.total);
    setLoading(false);
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
  }, [page, pageSize, orderBy, order, month]);

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
      <Grid container spacing={3}>
        <Grid item xs={4}>
          <TextField
            size="small"
            sx={{ mb: 3 }}
            label="Search"
            value={search}
            onChange={(e) => setSearch(e.target.value)}
          />
          <Button sx={{ ml: 1 }} variant="contained" onClick={getJourneys}>
            Search
          </Button>
        </Grid>
        <Grid item xs={4}>
          <FormControl>
            <InputLabel>Month</InputLabel>
            <Select
              label="Month"
              size="small"
              value={month}
              onChange={(e) => setMonth(e.target.value)}
            >
              <MenuItem value={0}>All</MenuItem>
              <MenuItem value={5}>May</MenuItem>
              <MenuItem value={6}>June</MenuItem>
              <MenuItem value={7}>July</MenuItem>
            </Select>
          </FormControl>
        </Grid>
      </Grid>
      {loading ? (
        <Box sx={{ width: "100%" }}>
          <LinearProgress />
        </Box>
      ) : null}
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
            {journeys && !loading
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
