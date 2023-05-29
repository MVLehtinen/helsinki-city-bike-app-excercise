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
  TextField,
  Button,
  LinearProgress,
  Box,
} from "@mui/material";
import { useEffect, useState } from "react";
import { Link } from "react-router-dom";

function Stations() {
  const [stations, setStations] = useState(null);
  const [total, setTotal] = useState(0);
  const [page, setPage] = useState(1);
  const [pageSize, setPageSize] = useState(10);
  const [search, setSearch] = useState("");
  const [loading, setLoading] = useState(true);

  function constructQueryString() {
    let query = `?page=${page}&pageSize=${pageSize}`;
    if (search !== null && search.trim() !== "") query += `&search=${search}`;
    return query;
  }

  async function getStations() {
    setLoading(true);
    const queryString = constructQueryString();
    const response = await fetch(
      "http://localhost:5000/api/stations" + queryString
    );
    const data = await response.json();
    setStations(data.result);
    setTotal(data.total);
    setLoading(false);
  }

  function handlePageChange(e, newPage) {
    setPage(newPage + 1);
  }

  function handleRowsPerPageChange(e) {
    setPageSize(parseInt(e.target.value));
    setPage(1);
  }

  function executeSearch() {
    setPage(1);
    getStations();
  }

  useEffect(() => {
    getStations();
  }, [page, pageSize]);

  return (
    <>
      <Typography variant="h4" sx={{ mb: 5, mt: 5 }}>
        Stations
      </Typography>

      <TextField
        size="small"
        sx={{ mb: 3 }}
        label="Search"
        value={search}
        onChange={(e) => setSearch(e.target.value)}
      />
      <Button sx={{ ml: 1 }} variant="contained" onClick={executeSearch}>
        Search
      </Button>
      {loading ? (
        <Box sx={{ width: "100%" }}>
          <LinearProgress />
        </Box>
      ) : null}
      <TableContainer component={Paper}>
        <Table size="small">
          <TableHead>
            <TableRow>
              <TableCell>Nimi</TableCell>
              <TableCell>Namn</TableCell>
              <TableCell>Osoite</TableCell>
              <TableCell>Adress</TableCell>
              <TableCell>Kaupunki</TableCell>
              <TableCell>Stad</TableCell>
              <TableCell>Capacity</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {stations && !loading
              ? stations.map((s) => (
                  <TableRow key={s.id}>
                    <TableCell>
                      <Link
                        to={`/stations/${s.id}`}
                        style={{ textDecoration: "none" }}
                      >
                        {s.nimi}
                      </Link>
                    </TableCell>
                    <TableCell>
                      <Link
                        to={`/stations/${s.id}`}
                        style={{ textDecoration: "none" }}
                      >
                        {s.namn}
                      </Link>
                    </TableCell>
                    <TableCell>{s.osoite}</TableCell>
                    <TableCell>{s.adress}</TableCell>
                    <TableCell>{s.kaupunki}</TableCell>
                    <TableCell>{s.stad}</TableCell>
                    <TableCell>{s.kapasiteetti}</TableCell>
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

export default Stations;
