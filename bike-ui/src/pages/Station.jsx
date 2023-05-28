import { Box, Typography } from "@mui/material";
import { useParams } from "react-router-dom";
import { useEffect, useState } from "react";

function Station() {
  const { id } = useParams();

  const [station, setStation] = useState(null);
  const [stationDetails, setStationDetails] = useState(null);

  async function getStation() {
    const stationResponse = await fetch(
      "http://localhost:5000/api/stations/" + id
    );
    const stationDetailsResponse = await fetch(
      "http://localhost:5000/api/stations/" + id + "/details"
    );
    const stationData = await stationResponse.json();
    const stationDetailsData = await stationDetailsResponse.json();
    setStation(stationData);
    setStationDetails(stationDetailsData);
  }

  useEffect(() => {
    getStation();
  }, []);

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
          <Typography>
            Koordinaatit: X: {parseFloat(station.x).toFixed(4)}, Y:{" "}
            {parseFloat(station.y).toFixed(4)}
          </Typography>
        </>
      ) : null}
      {stationDetails ? (
        <>
          <Typography>
            Average distance of departure:{" "}
            {parseFloat(stationDetails.averageDistanceOfDeparture).toFixed(1)}{" "}
            meters
          </Typography>
          <Typography>
            Average distance of return:{" "}
            {parseFloat(stationDetails.averageDistanceOfReturn).toFixed(1)}{" "}
            meters
          </Typography>
          <Typography>
            Total departures: {stationDetails.totalDepartures}
          </Typography>
          <Typography>Total returns: {stationDetails.totalReturns}</Typography>
          <Typography>Top 5 Destinations</Typography>
          <Box sx={{ ml: 2 }}>
            {stationDetails.top5Destinations.map((d, i) => (
              <Typography>{`${i + 1}. ${d.item.nimi}, ${d.total}`}</Typography>
            ))}
          </Box>
          <Typography>Top 5 Origins</Typography>
          <Box sx={{ ml: 2 }}>
            {stationDetails.top5Origins.map((d, i) => (
              <Typography>{`${i + 1}. ${d.item.nimi}, ${d.total}`}</Typography>
            ))}
          </Box>
        </>
      ) : null}
    </>
  );
}

export default Station;
