import { Box, Typography } from '@mui/material';
import { useState, useEffect } from 'react';
import axios from '../../api/axios';

const ProfileCard = () => {
  const [userData, setUserData] = useState({
    username: '',
    about: '',
    profileImage: ''
  });

  useEffect(() => {
    const fetchUserData = async () => {
      const token = localStorage.getItem('token'); 
      const userId = localStorage.getItem('userId'); 

      if (token && userId) {
        try {
          const response = await axios.get(`/Users/${userId}`, {
            headers: {
              Authorization: `Bearer ${token}`  
            }
          });

          const user = response.data;
          setUserData({
            username: user.username,
            about: user.about || 'No bio available', 
            profileImage: user.image ? user.image.imagePath : 'https://example.com/default-image.png' 
          });
        } catch (error) {
          console.error('Error fetching user data:', error);
        }
      }
    };

    fetchUserData();
  }, []); 

  return (
    <Box
      sx={{
        height: '42%',
        width: '60%',
        textAlign: 'center',
        borderRadius: '25px',
        backgroundColor: '#f0f0f0',
        display: 'flex',
        flexDirection: 'column',
        justifyContent: 'center',
        alignItems: 'center',
        padding: '20px',
      }}
    >
      <img
        src={userData.profileImage}
        width="40%"
        height="55%"
        style={{
          borderRadius: '50%',
        }}
        alt="Profile"
      />
      <Typography variant="h6" sx={{ marginTop: '10px', fontWeight: 'bold' }}>
        {userData.username || 'Loading...'}
      </Typography>
      <Typography sx={{ marginTop: '10px' }}>
        {userData.about}
      </Typography>
    </Box>
  );
};

export default ProfileCard;
