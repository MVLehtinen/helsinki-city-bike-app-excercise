import { Container, Divider, Grid, Typography } from "@mui/material";
import { Link } from "react-router-dom";

function Header() {
  return (
    <>
      <Typography variant="h2" gutterBottom>
        Helsinki City Bike App
      </Typography>
      <Grid spacing={3} container sx={{ mb: 3 }}>
        <Grid item>
          <Link to="/home">Home</Link>
        </Grid>
        <Grid item>
          <Link to="/journeys">Journeys</Link>
        </Grid>
        <Grid item>
          <Link to="/stations">Stations</Link>
        </Grid>
      </Grid>
      <Divider />
    </>
  );
}

export default Header;
