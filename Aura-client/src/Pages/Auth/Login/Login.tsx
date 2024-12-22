import Box from '@mui/material/Box';
import Grid from '@mui/material/Grid';
import RightSide from './RightSide';
import LeftSide from './LeftSide';

const Login = () => {
  return (
    <Box sx={{ flexGrow: 1,height:"100vh" }}>
      <Grid container>
        <Grid item xs={12} lg={6}>
          <LeftSide />
        </Grid>

        <Grid item lg={6}>
          <RightSide />
        </Grid>
      </Grid>
    </Box>
  );
};

export default Login;
