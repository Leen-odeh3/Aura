import Box from '@mui/material/Box';
import Grid from '@mui/material/Grid';
import RightSide from './RightSide';
import LeftSide from './LeftSide';

const Home = () => {
  return (
    <Box sx={{ flexGrow: 1, height: "100vh" }}>
      <Grid container>
        <Grid item xs={12} lg={5}>
          <LeftSide />
        </Grid>

        <Grid item lg={7}>
          <RightSide />
        </Grid>
      </Grid>
    </Box>
  );
};

export default Home;
