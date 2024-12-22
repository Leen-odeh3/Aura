import React from "react";
import { Box, Typography } from "@mui/material";
import { Home, People, Mail, Notifications} from "@mui/icons-material";
import PersonAddIcon from '@mui/icons-material/PersonAdd';
import { useNavigate } from "react-router-dom";
import { NavItem } from "../../Interfaces/NavItem"; 

const navItems: NavItem[] = [
  { text: "HomePage", route: "/home", icon: <Home sx={{ fontSize: 17 }} /> },
  { text: "Following", route: "/following", icon: <PersonAddIcon sx={{ fontSize: 17 }} /> },
  { text: "Followers", route: "/followers", icon: <People sx={{ fontSize: 17 }} /> },
  { text: "Messages", route: "/messages", icon: <Mail sx={{ fontSize: 17 }} /> },
  { text: "Notifications", route: "/notifications", icon: <Notifications sx={{ fontSize: 17}} /> },
];


const Navbar: React.FC = () => {
  const navigate = useNavigate();

  return (
    <Box sx={{ display: "flex", flexDirection: "row", width: "200px", padding: "10px",alignItems:"center"}}>
      {navItems.map((item, index) => (
        <Box
          key={index}
          sx={{
            display: "flex",
            alignItems: "center",
            padding: "10px 15px",
            cursor: "pointer",
            borderRadius: "8px",
            transition: "background-color 0.3s",
            "&:hover": {
              backgroundColor: "#f0f0f0",
            },
          }}
          onClick={() => navigate(item.route)}
        >
          {item.icon && <Box sx={{ marginRight: "3px", color: "var(--Primary)",fontSize:"small"}}>{item.icon}</Box>}
          <Typography variant="body1" sx={{fontFamily:"Open Sans, sans-serif",color:"#565657"}}>{item.text}</Typography>
        </Box>
      ))}
    </Box>
  );
};

export default Navbar;
