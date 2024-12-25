import { Box } from "@mui/material"
import Feed from "../../Component/Feed/Feed"
import Header from "../../Component/Header/Header";

const Dashboard = () => {
  return (
    <Box sx={{height:"100vh", overflowY:"hidden"}}>
      <Header/>
      <Feed/>
    </Box>
  )
}

export default Dashboard
