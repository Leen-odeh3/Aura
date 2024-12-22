import { Box, Typography } from '@mui/material';
import photo from '../../../public/Images/Facebook.png';

const ProfileCard = () => {
  return (
    <>
    <Box
      sx={{
        height: "60%",
        width: "70%",
        textAlign: "center",
        borderRadius: "25px",
       backgroundColor: "#f0f0f0",
        display: "flex",
        flexDirection: "column",
        justifyContent: "center", 
        alignItems: "center", 
        padding: "20px", 
      }}
    >
      <img
        src={photo}
        width="30%"
          height="30%"
          style={{
            borderRadius: "50%",
          }}
          alt="Profile"
        />
        <Typography variant="h6" sx={{ marginTop: "10px",fontWeight:"bold"}}>
          Leen Odeh
        </Typography>
        <Typography sx={{ marginTop: "10px" }}>
          Fullstack developer,software engineer..
        </Typography>
      </Box>
      <Box sx={{ fontSize: "10px" }}>
        <Typography className="link">Privacy Terms </Typography>
        <Typography className="link">Advirtising </Typography>
        <Typography className="link">Cookies</Typography>
        <Typography className="link">Aura @ 2025</Typography>
      </Box>
    </>
  );
};

export default ProfileCard;
