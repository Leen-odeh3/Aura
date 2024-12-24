import { Box, Button, TextField, Typography, Avatar, Snackbar } from '@mui/material';
import { useState, useEffect } from 'react';
import axios from '../../api/axios';

const Profile = () => {
  const [userData, setUserData] = useState({
    username: '',
    about: '',
    profileImage: ''
  });

  const [newImage, setNewImage] = useState(null);
  const [newAbout, setNewAbout] = useState('');
  const [isEditingAbout, setIsEditingAbout] = useState(false);
  const [snackbarOpen, setSnackbarOpen] = useState(false);
  const [snackbarMessage, setSnackbarMessage] = useState('');
  const [snackbarSeverity, setSnackbarSeverity] = useState('success');

  const userId = localStorage.getItem('userId');

  useEffect(() => {
    const fetchUserData = async () => {
      const token = localStorage.getItem('token');

      if (token && userId) {
        try {
          const response = await axios.get(`/Users/${userId}`, {
            headers: { Authorization: `Bearer ${token}` }
          });

          const user = response.data;
          setUserData({
            username: user.username,
            about: user.about || 'No bio available',
            profileImage: user.image ? user.image.imagePath : 'https://via.placeholder.com/150'
          });
          setNewAbout(user.about || ''); // Set the initial value of the "about" field
        } catch (error) {
          console.error('Error fetching user data:', error);
        }
      }
    };

    fetchUserData();
  }, [userId]);

  const handleImageChange = (e) => {
    setNewImage(e.target.files[0]);
  };

  const handleAddImage = async () => {
    const token = localStorage.getItem('token');

    if (token && userId && newImage) {
      try {
        const formData = new FormData();
        formData.append('image', newImage);

        const response = await axios.post(`/Users/${userId}/profile-image`, formData, {
          headers: {
            Authorization: `Bearer ${token}`,
            'Content-Type': 'multipart/form-data'
          }
        });

        setUserData((prev) => ({
          ...prev,
          profileImage: response.data.imagePath
        }));

        setNewImage(null);
        setSnackbarMessage('Image added successfully!');
        setSnackbarSeverity('success');
        setSnackbarOpen(true);
      } catch (error) {
        console.error('Error adding profile image:', error);
        setSnackbarMessage('Error adding image!');
        setSnackbarSeverity('error');
        setSnackbarOpen(true);
      }
    }
  };

  const handleRemoveImage = async () => {
    const token = localStorage.getItem('token');

    if (token && userId) {
      try {
        await axios.delete(`/Users/${userId}/profile-image`, {
          headers: { Authorization: `Bearer ${token}` }
        });

        setUserData((prev) => ({
          ...prev,
          profileImage: 'https://via.placeholder.com/150'
        }));
        setSnackbarMessage('Image removed successfully!');
        setSnackbarSeverity('success');
        setSnackbarOpen(true);
      } catch (error) {
        console.error('Error deleting profile image:', error);
        setSnackbarMessage('Error removing image!');
        setSnackbarSeverity('error');
        setSnackbarOpen(true);
      }
    }
  };

  const handleUpdateAbout = async () => {
    const token = localStorage.getItem('token');

    if (token && userId && newAbout !== userData.about) {
      try {
        await axios.put(`/Users/${userId}/about`, { about: newAbout }, {
          headers: { Authorization: `Bearer ${token}` }
        });

        setUserData((prev) => ({
          ...prev,
          about: newAbout
        }));

        setIsEditingAbout(false);
        setSnackbarMessage('About section updated successfully!');
        setSnackbarSeverity('success');
        setSnackbarOpen(true);
      } catch (error) {
        console.error('Error updating about section:', error);
        setSnackbarMessage('Error updating about section!');
        setSnackbarSeverity('error');
        setSnackbarOpen(true);
      }
    }
  };

  const handleCloseSnackbar = () => {
    setSnackbarOpen(false);
  };

  return (
    <Box sx={{
      maxWidth: '600px',
      margin: '20px auto',
      padding: '20px',
      textAlign: 'center',
      borderRadius: '12px',
      boxShadow: '0 8px 16px rgba(0, 0, 0, 0.1)',
      backgroundColor: '#fff',
      display: 'flex',
      flexDirection: 'column',
      alignItems: 'center',
      justifyContent: 'center',
      minHeight: '80vh',
    }}>
      <Typography variant="h4" sx={{ marginBottom: '20px', fontWeight: 'bold', color: '#333' }}>
        Profile
      </Typography>

      <Avatar
        src={userData.profileImage}
        alt="Profile"
        sx={{
          width: 150,
          height: 150,
          borderRadius: '50%',
          marginBottom: '20px',
          cursor: 'pointer',
          transition: 'all 0.3s ease',
          '&:hover': { transform: 'scale(1.1)' }
        }}
      />

      <Box sx={{ marginBottom: '20px', width: '100%' }}>
        <Typography variant="h6" sx={{ color: '#555', marginBottom: '10px' }}>
          About
        </Typography>
        {isEditingAbout ? (
          <>
            <TextField
              value={newAbout}
              onChange={(e) => setNewAbout(e.target.value)}
              fullWidth
              multiline
              rows={4}
              variant="outlined"
              sx={{ marginBottom: '10px' }}
            />
            <Button variant="contained" color="primary" onClick={handleUpdateAbout}>
              Save About
            </Button>
          </>
        ) : (
          <>
            <Typography variant="body1" sx={{ marginBottom: '10px' }}>
              {userData.about}
            </Typography>
            <Button variant="contained" color="secondary" onClick={() => setIsEditingAbout(true)}>
              Edit About
            </Button>
          </>
        )}
      </Box>

      <Box sx={{ marginTop: '20px', display: 'flex', justifyContent: 'center', width: '100%' }}>
        <Button variant="contained" color="primary" sx={{ marginRight: '10px' }} onClick={() => document.getElementById('image-upload').click()}>
          Add Image
        </Button>
        <input
          type="file"
          id="image-upload"
          accept="image/*"
          onChange={handleImageChange}
          style={{ display: 'none' }}
        />
        <Button variant="contained" color="error" sx={{ marginRight: '10px' }} onClick={handleRemoveImage}>
          Remove Image
        </Button>
        <Button variant="contained" color="secondary" onClick={handleAddImage} disabled={!newImage}>
          Save Image
        </Button>
      </Box>

      <Snackbar
        open={snackbarOpen}
        autoHideDuration={6000}
        onClose={handleCloseSnackbar}
        message={snackbarMessage}
        severity={snackbarSeverity}
      />
    </Box>
  );
};

export default Profile;
