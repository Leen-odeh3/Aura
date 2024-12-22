import { Box, Grid } from "@mui/material";
import Logo from '../../../public/Images/Logo.png'
import Navbar from "./Navbar";
const Header = () => {
  return (
    <Box>
      <Grid
        container
        sx={{ display: "flex", justifyContent: "center", alignItems: "center", padding: "10px 20px" }}
      >
        <Grid item xs={12} lg={2} sx={{ textAlign: "center" }}>
          <img src={Logo} width="30%" height="30%" alt="Auro Logo" />
        </Grid>
        <Grid item lg={3} sx={{ textAlign: "center" }}>
          <div className="middle-header-search">
            <input
              type="search"
              placeholder="search"
              className="input-search"
            />
          </div>
        </Grid>
        <Grid item xs={12} lg={7} sx={{ textAlign: "center", paddingLeft: "10px" }}>
          <Navbar />
        </Grid>
      </Grid>
    </Box>
  );
};

export default Header;

