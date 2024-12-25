import { Box, Grid} from "@mui/material";
import ProfileCard from "./ProfileCard";
import Post from "../Post/Post";

const Feed = () => {
  return (
    <Box sx={{ padding: "20px", height: "89vh" }}>
      <Grid container sx={{ height: "80vh" }}>
        <Grid item xs={12} sm={3}>
          <ProfileCard />
        </Grid>

        <Grid xs={12} sm={9}>
          <Post/>
        </Grid>
      </Grid>
    </Box>
  );
};

export default Feed;

