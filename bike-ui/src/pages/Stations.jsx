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
} from "@mui/material";
import { useEffect, useState } from "react";

function Stations() {
  const [stations, setStations] = useState(null);
  const [total, setTotal] = useState(0);
  const [page, setPage] = useState(1);
  const [pageSize, setPageSize] = useState(10);

  function constructQueryString() {
    let query = `?page=${page}&pageSize=${pageSize}`;
    return query;
  }

  async function getStations() {
    const queryString = constructQueryString();
    const response = await fetch(
      "http://localhost:5000/api/stations" + queryString
    );
    const data = await response.json();
    setStations(data.result);
    setTotal(data.total);
  }

  function handlePageChange(e, newPage) {
    setPage(newPage + 1);
  }

  function handleRowsPerPageChange(e) {
    setPageSize(parseInt(e.target.value));
    setPage(1);
  }

  useEffect(() => {
    getStations();
  }, [page, pageSize]);

  return (
    <>
      <Typography variant="h4" sx={{ mb: 5, mt: 5 }}>
        Stations
      </Typography>
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
            {stations
              ? stations.map((s) => (
                  <TableRow key={s.id}>
                    <TableCell>{s.nimi}</TableCell>
                    <TableCell>{s.namn}</TableCell>
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
