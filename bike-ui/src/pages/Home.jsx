import { Typography } from "@mui/material";

function Home() {
  return (
    <>
      <Typography variant="h4" sx={{ mb: 5, mt: 5 }}>
        Welcome to Helsinki City Bike App
      </Typography>
      <Typography>
        This is an app made for Solita Dev Academy excercise. It's a fun little
        app that displays bike journeys and lists stations using open data from
        HSL. Go check the listings and have fun!
      </Typography>
    </>
  );
}

export default Home;
