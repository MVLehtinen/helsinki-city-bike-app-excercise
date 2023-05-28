import { Container, Divider, Grid, Link, Typography } from "@mui/material";
import { BrowserRouter, Routes, Route, Outlet } from "react-router-dom";
import Home from "./pages/Home";
import Stations from "./pages/Stations";
import Journeys from "./pages/Journeys";
import Header from "./components/Header";

function App() {
  return (
    <Container>
      <BrowserRouter>
        <Routes>
          <Route
            path="/"
            element={
              <>
                <Header />
                <Outlet />
              </>
            }
          >
            <Route index element={<Home />} />
            <Route path="home" element={<Home />} />
            <Route path="stations" element={<Stations />} />
            <Route path="journeys" element={<Journeys />} />
          </Route>
        </Routes>
      </BrowserRouter>
    </Container>
  );
}

export default App;
