import { Box, Grid } from "@mui/material";
import ProfileCard from "./ProfileCard";
import Post from "../Post/Post";
import UserPosts from "../../Pages/Profile/UserPosts";
import Story from "../Story/Story";

const Feed = () => {
  return (
    <Box sx={{ padding: "20px", height: "89vh" }}>
      <Grid container sx={{ height: "80vh" }}>
        <Grid item xs={12} sm={3} sx={{ position: "fixed", zIndex: 10 }}>
          <ProfileCard />
        </Grid>

        <Grid xs={12} sm={9} sx={{marginLeft: "300px",
           paddingTop: "20px",height: "85vh", overflowY: "scroll", maxWidth:"600px"}}>
            <Story/>
            <Post />
            <UserPosts />
        </Grid>
      </Grid>
    </Box>
  );
};

export default Feed;

