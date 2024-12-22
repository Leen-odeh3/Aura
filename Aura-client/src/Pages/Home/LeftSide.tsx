import { Box } from '@mui/material';
import logo from '/Images/MainImage.png';

const LeftSide = () => {
  return (
    <Box sx={{
      display: "flex",
      justifyContent: "center",
      alignItems: "center",
      height: "100vh",
    }}>
      <img src={logo} alt="Logo" style={{ maxWidth: "100%", maxHeight: "100%" }} />
    </Box>
  );
};

export default LeftSide;
