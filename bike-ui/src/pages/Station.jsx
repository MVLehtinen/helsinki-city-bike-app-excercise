import {
  Box,
  Typography,
  Table,
  TableRow,
  TableCell,
  Select,
  MenuItem,
  InputLabel,
  FormControl,
  Grid,
  CircularProgress,
} from "@mui/material";
import { useParams } from "react-router-dom";
import { useEffect, useState } from "react";

function Station() {
  const { id } = useParams();

  const [station, setStation] = useState(null);
  const [stationDetails, setStationDetails] = useState(null);
  const [month, setMonth] = useState(0);
  const [loading, setLoading] = useState(true);

  function createMonthQueryString() {
    if (month == 0) return "";
    return `?month=${month}`;
  }

  async function getStationDetails() {
    setLoading(true);
    const monthQuery = createMonthQueryString();
    const stationDetailsResponse = await fetch(
      "http://localhost:5000/api/stations/" + id + "/details" + monthQuery
    );
    const stationDetailsData = await stationDetailsResponse.json();
    setStationDetails(stationDetailsData);
    setLoading(false);
  }

  async function getStation() {
    const stationResponse = await fetch(
      "http://localhost:5000/api/stations/" + id
    );
    const stationData = await stationResponse.json();
    setStation(stationData);
  }

  useEffect(() => {
    getStation();
  }, []);

  useEffect(() => {
    getStationDetails();
  }, [month]);

  function handleMonthChange(e) {
    setMonth(e.target.value, getStationDetails);
  }

  return (
    <>
      {station ? (
        <>
          <Typography variant="h4" sx={{ mb: 2, mt: 5 }}>
            {station.nimi}
          </Typography>
          <Typography variant="h5" sx={{ mb: 2 }}>
            {station.namn}
            {station.nimi !== station.name ? `, ${station.name}` : null}
          </Typography>
          <Typography>Osoite: {station.osoite}</Typography>
          <Typography>Adress: {station.adress}</Typography>
          <Typography>Kaupunki: {station.kaupunki}</Typography>
          <Typography>Stad: {station.stad}</Typography>
          <Typography>Operaattori: {station.operaattori}</Typography>
          <Typography>Koordinaatit:</Typography>
          <Box sx={{ ml: 2 }}>
            <Typography>X: {parseFloat(station.x).toFixed(4)}</Typography>
            <Typography>Y: {parseFloat(station.y).toFixed(4)}</Typography>
          </Box>
        </>
      ) : null}
      <Grid container spacing={3} sx={{ mt: 1 }}>
        <Grid item xs={2}>
          <Typography variant="h6">Details</Typography>
        </Grid>
        <Grid item>
          <FormControl>
            <InputLabel>Month</InputLabel>
            <Select
              label="Month"
              size="small"
              value={month}
              onChange={handleMonthChange}
            >
              <MenuItem value={0}>All</MenuItem>
              <MenuItem value={5}>May</MenuItem>
              <MenuItem value={6}>June</MenuItem>
              <MenuItem value={7}>July</MenuItem>
            </Select>
          </FormControl>
        </Grid>
      </Grid>
      {stationDetails && !loading ? (
        <>
          <Typography sx={{ mt: 2 }}>Top 5 Destinations</Typography>
          <Box sx={{ ml: 2 }}>
            {stationDetails.top5Destinations.map((d, i) => (
              <Typography key={i}>{`${i + 1}. ${d.item.nimi}, ${
                d.total
              }`}</Typography>
            ))}
          </Box>
          <Typography sx={{ mt: 2 }}>Top 5 Origins</Typography>
          <Box sx={{ ml: 2 }}>
            {stationDetails.top5Origins.map((d, i) => (
              <Typography key={i}>{`${i + 1}. ${d.item.nimi}, ${
                d.total
              }`}</Typography>
            ))}
          </Box>
          <Table sx={{ maxWidth: "500px", mt: 3 }} size="small">
            <TableRow>
              <TableCell>Average distance of departure:</TableCell>
              <TableCell>
                {parseFloat(stationDetails.averageDistanceOfDeparture).toFixed(
                  1
                )}{" "}
                meters
              </TableCell>
            </TableRow>
            <TableRow>
              <TableCell>Average distance of return:</TableCell>
              <TableCell>
                {parseFloat(stationDetails.averageDistanceOfReturn).toFixed(1)}{" "}
                meters
              </TableCell>
            </TableRow>
            <TableRow>
              <TableCell>Total departures:</TableCell>
              <TableCell>{stationDetails.totalDepartures}</TableCell>
            </TableRow>
            <TableRow>
              <TableCell>Total returns:</TableCell>
              <TableCell>{stationDetails.totalReturns}</TableCell>
            </TableRow>
          </Table>
        </>
      ) : (
        <CircularProgress />
      )}
    </>
  );
}

export default Station;
