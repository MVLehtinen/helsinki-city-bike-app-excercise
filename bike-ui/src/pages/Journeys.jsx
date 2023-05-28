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

function Journeys() {
  const [journeys, setJourneys] = useState(null);
  const [total, setTotal] = useState(0);
  const [page, setPage] = useState(1);
  const [pageSize, setPageSize] = useState(10);

  function constructQueryString() {
    let query = `?page=${page}&pageSize=${pageSize}`;
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
  }, [page, pageSize]);

  return (
    <>
      <Typography variant="h4" sx={{ mb: 5, mt: 5 }}>
        Journeys
      </Typography>
      <TableContainer component={Paper}>
        <Table size="small">
          <TableHead>
            <TableRow>
              <TableCell>Departure time</TableCell>
              <TableCell>Return time</TableCell>
              <TableCell>Departure station</TableCell>
              <TableCell>Return station</TableCell>
              <TableCell>Covered distance</TableCell>
              <TableCell>Duration</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {journeys ? (
              <>
                {journeys.map((j) => (
                  <TableRow key={j.id}>
                    <TableCell>{getPrettyDateString(j.departure)}</TableCell>
                    <TableCell>{getPrettyDateString(j.return)}</TableCell>
                    <TableCell>{j.departureStationName}</TableCell>
                    <TableCell>{j.returnStationName}</TableCell>
                    <TableCell>{j.coveredDistance} m</TableCell>
                    <TableCell>{j.duration}</TableCell>
                  </TableRow>
                ))}
              </>
            ) : null}
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
