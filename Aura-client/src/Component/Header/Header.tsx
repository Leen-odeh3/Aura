import { Box, Grid } from "@mui/material";
import Logo from '../../../public/Images/Logo.png'
import Navbar from "./Navbar";
const Header = () => {
  return (
    <Box>
      <Grid
        container
        sx={{ display: "flex", justifyContent: "center", alignItems: "center" }}
      >
        <Grid item xs={12} lg={1}>
          <img src={Logo} width="50%" height="50%" alt="Auro Logo" />
        </Grid>
        <Grid item lg={3}>
          <div className="middle-header-search">
            <input
              type="search"
              placeholder="search"
              className="input-search"
            />
          </div>
        </Grid>
        <Grid item xs={12} lg={7} sx={{ paddingLeft: "10px" }}>
          <Navbar />
        </Grid>
      </Grid>
    </Box>
  );
};

export default Header;

