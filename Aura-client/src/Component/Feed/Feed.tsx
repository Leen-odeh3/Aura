import { Box, Grid} from "@mui/material";
import ProfileCard from "./ProfileCard";

const Feed = () => {
  return (
    <Box sx={{ padding: "20px", height: "89vh" }}>
      <Grid container spacing={2} sx={{ height: "80vh" }}>
        <Grid item xs={12} sm={4}>
          <ProfileCard />
        </Grid>

        <Grid item xs={12} sm={8}>
          leen
        </Grid>
      </Grid>
    </Box>
  );
};

export default Feed;

