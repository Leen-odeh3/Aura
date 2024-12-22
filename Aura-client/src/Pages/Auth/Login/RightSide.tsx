import { Typography } from '@mui/material';
import Box from '@mui/material/Box';
import Buttons from '../../../Component/Buttons/Buttons';
import facebook from '../../../../public/Images/facebook.png';
import Google from '../../../../public/Images/google.jpg';

const RightSide = () => {
  return (
    <Box sx={{
      flexGrow: 1, display: "flex", height: "100vh",
      justifyContent: "center",
      flexDirection: "column", alignItems: "center", padding: "20px"
    }}>
      <Box>
        <Typography sx={{ fontWeight: "900", fontSize: "30px", marginBottom: "10px" }}>Discover What's Next </Typography>
        <Typography sx={{ fontSize: "30px", fontWeight: "900", marginBottom: "13px" }}>Join the <span>Aura </span>today</Typography>
      </Box>
      <Box>
        <Buttons text="Sign up with Phone Or email" img={''} />
      </Box>
      <span style={{ color: "black", marginBottom: "10px" }}>OR</span>
      <Box sx={{ flexDirection: "column", display: "flex" }}>
        <Buttons img={facebook} text="Sign up with Facebook" />
        <Buttons img={Google} text="Sign up with Google" />
      </Box>
      <Box sx={{ display: "flex", flexDirection: "column" }}>
        <Typography sx={{ fontWeight: "bold", marginBottom: "10px" }}>Already have an account?</Typography>
        <Buttons text="Sign in" img={''} />
      </Box>
      <Typography sx={{ fontSize: "12px" }}>
        By signing up, you agree to the <span style={{ fontWeight: "initial" }}>
          Terms of Service </span> and <span style={{ fontWeight: "initial" }}>
          Privacy Policy</span>, including <span style={{ fontWeight: "initial" }}>
          Cookie </span> Use.</Typography>

    </Box>
  );
};

export default RightSide;
