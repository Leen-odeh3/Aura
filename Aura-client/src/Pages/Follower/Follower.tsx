import { Box, Typography, List, ListItem, ListItemAvatar, Avatar, ListItemText } from '@mui/material';
import { useState, useEffect } from 'react';
import axios from '../../api/axios'; 
import { useNavigate } from 'react-router-dom';
import Header from '../../Component/Header/Header';

const Follower = () => {
  const [followers, setFollowers] = useState<any[]>([]);
  const [error, setError] = useState<string | null>(null);

  const navigate = useNavigate();

  useEffect(() => {
    const fetchFollowers = async () => {
      const token = localStorage.getItem('token');

      if (token) {
        try {
          const userId = localStorage.getItem('userId'); 
          if (userId) {
            const response = await axios.get(`/Users/${userId}/followers`, {
              headers: {
                Authorization: `Bearer ${token}`, 
              },
            });
            setFollowers(response.data);
          }
        } catch (err) {
          setError('Error fetching followers');
          console.error('Error fetching followers:', err);
        }
      } else {
        setError('No authentication token found'); 
      }
    };

    fetchFollowers();
  }, []); 

  return (
    <>
    <Header/>
    <Box sx={{ maxWidth: 800, margin: 'auto', paddingTop: 2 }}>
      <Typography variant="h5" gutterBottom sx={{marginTop:"20px"}}>
        Followers
      </Typography>
      {error && <Typography color="error">{error}</Typography>} 
      <List>
        {followers.length > 0 ? (
          followers.map((follower) => (
            <ListItem key={follower.id} 
            sx={{borderRadius:4, padding: 2 , border:"1px solid #e3e1e1",
              display: 'flex', width:"250px",flexDirection: 'column', alignItems: 'center',marginBottom:"20px"}}>
              <ListItemAvatar>
              <Avatar src={follower.image?.imagePath || '/default-avatar.jpg'} />
              </ListItemAvatar>
              <ListItemText
                primary={follower.username}
                secondary={follower.about || 'No bio available'}
              />
            </ListItem>
          ))
        ) : (
          <Typography variant="h6">No followers found.</Typography> 
        )}
      </List>
    </Box>
    </>
  );
};

export default Follower;

