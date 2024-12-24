import { Box, Typography, Button, TextField, Modal } from '@mui/material';
import { useState, useEffect } from 'react';
import axios from '../../api/axios';
import { useNavigate } from 'react-router-dom';

const Profile = () => {
  const [userData, setUserData] = useState({
    username: '',
    about: '',
    profileImage: ''
  });
  const [isEditing, setIsEditing] = useState(false);
  const [newAbout, setNewAbout] = useState('');
  const [openModal, setOpenModal] = useState(false); 
  const [imageUrl, setImageUrl] = useState(''); 
  const navigate = useNavigate();

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
            profileImage: user.image ? user.image.imagePath : 'https://via.placeholder.com/150'
          });
        } catch (error) {
          console.error('Error fetching user data:', error);
        }
      }
    };

    fetchUserData();
  }, []);

  const handleEditClick = () => {
    setIsEditing(true);
    setNewAbout(userData.about); 
  };

  const handleCancelClick = () => {
    setIsEditing(false);
    setNewAbout(''); 
  };

  const handleSaveClick = async () => {
    const token = localStorage.getItem('token');
    const userId = localStorage.getItem('userId');
    
    if (token && userId && newAbout.trim() !== '') {
      try {
        const response = await axios.put(
          `/Users/${userId}/about`,
          { newAbout },
          {
            headers: {
              Authorization: `Bearer ${token}`
            }
          }
        );
        
        if (response.status === 200) {
          setUserData((prevData) => ({
            ...prevData,
            about: newAbout 
          }));
          setIsEditing(false); 
        }
      } catch (error) {
        console.error('Error updating bio:', error);
      }
    }
  };

  const handleProfileImageChange = async (e) => {
    const file = e.target.files[0];
    if (file) {
      const token = localStorage.getItem('token');
      const userId = localStorage.getItem('userId');
      
      const formData = new FormData();
      formData.append('image', file);

      try {
        const response = await axios.post(`/Users/${userId}/profile-image`, formData, {
          headers: {
            'Authorization': `Bearer ${token}`,
            'Content-Type': 'multipart/form-data'
          }
        });

        if (response.status === 200) {
          setUserData((prevData) => ({
            ...prevData,
            profileImage: response.data.imagePath 
          }));
        }
      } catch (error) {
        console.error('Error uploading image:', error);
      }
    }
  };

  const handleDeleteImage = async () => {
    const token = localStorage.getItem('token');
    const userId = localStorage.getItem('userId');
    
    if (token && userId) {
      try {
        const response = await axios.delete(`/Users/${userId}/profile-image`, {
          headers: {
            Authorization: `Bearer ${token}`
          }
        });

        if (response.status === 200) {
          setUserData((prevData) => ({
            ...prevData,
            profileImage: 'https://via.placeholder.com/150'
          }));
        }
      } catch (error) {
        console.error('Error deleting image:', error);
      }
    }
  };

  const handleImageClick = () => {
    setImageUrl(userData.profileImage); 
    setOpenModal(true); 
  };

  const handleCloseModal = () => {
    setOpenModal(false); 
  };

  return (
    <Box
      sx={{
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
        padding: '20px',
        backgroundColor: '#fafafa',
        minHeight: '100vh',
      }}
    >
      <Typography variant="h4" sx={{ marginBottom: '20px', fontWeight: 'bold' }}>
        My Profile
      </Typography>

      <Box
        sx={{
          width: '150px',
          height: '150px',
          borderRadius: '50%',
          overflow: 'hidden',
          border: '3px solid #f76c5e',
          marginBottom: '20px',
          cursor: 'pointer',
        }}
        onClick={handleImageClick} 
      >
        <img
          src={userData.profileImage}
          alt="Profile"
          style={{
            width: '100%',
            height: '100%',
            objectFit: 'cover',
          }}
        />
      </Box>

      <input
        type="file"
        id="profileImageInput"
        style={{ display: 'none' }}
        accept="image/*"
        onChange={handleProfileImageChange}
      />

      <Box sx={{ display: 'flex', gap: '10px', marginBottom: '20px' }}>
        <Button
          variant="contained"
          sx={{
            backgroundColor: '#fa8072',
            '&:hover': {
              backgroundColor: '#f76c5e',
            },
            padding: '10px 20px',
            borderRadius: '25px',
          }}
          onClick={handleEditClick}
        >
          Edit
        </Button>

        <Button
          variant="contained"
          sx={{
            backgroundColor: '#f44336',
            '&:hover': {
              backgroundColor: '#e53935',
            },
            padding: '10px 20px',
            borderRadius: '25px',
          }}
          onClick={handleDeleteImage}
        >
          Delete
        </Button>
      </Box>

      <Box
        sx={{
          width: '100%',
          maxWidth: '600px',
          backgroundColor: '#fff',
          padding: '20px',
          borderRadius: '10px',
          boxShadow: '0px 4px 6px rgba(0,0,0,0.1)',
          marginBottom: '20px',
        }}
      >
        <Typography variant="h6" sx={{ fontWeight: 'bold' }}>
          Username:
        </Typography>
        <Typography sx={{ marginBottom: '15px' }}>
          {userData.username || 'Loading...'}
        </Typography>

        <Typography variant="h6" sx={{ fontWeight: 'bold' }}>
          About:
        </Typography>
        {/* Editable Bio */}
        {isEditing ? (
          <Box sx={{ marginBottom: '15px' }}>
            <TextField
              value={newAbout}
              onChange={(e) => setNewAbout(e.target.value)}
              fullWidth
              multiline
              rows={4}
              placeholder="Write your bio here..."
            />
            <Box sx={{ display: 'flex', gap: '10px', marginTop: '10px' }}>
              <Button
                variant="contained"
                sx={{
                  backgroundColor: '#f44336',
                  '&:hover': {
                    backgroundColor: '#e53935',
                  },
                  padding: '10px 20px',
                  borderRadius: '25px',
                }}
                onClick={handleCancelClick}
              >
                Cancel
              </Button>
              <Button
                variant="contained"
                sx={{
                  backgroundColor: '#4caf50',
                  '&:hover': {
                    backgroundColor: '#45a049',
                  },
                  padding: '10px 20px',
                  borderRadius: '25px',
                }}
                onClick={handleSaveClick}
              >
                Save
              </Button>
            </Box>
          </Box>
        ) : (
          <Typography sx={{ marginBottom: '15px' }}>
            {userData.about || 'No bio available'}
          </Typography>
        )}
      </Box>

      <Modal
        open={openModal}
        onClose={handleCloseModal}
        sx={{
          display: 'flex',
          justifyContent: 'center',
          alignItems: 'center',
        }}
      >
        <Box
          sx={{
            backgroundColor: '#fff',
            padding: '20px',
            borderRadius: '10px',
            boxShadow: '0px 4px 6px rgba(0,0,0,0.1)',
            display: 'flex',
            flexDirection: 'column',
            alignItems: 'center',
          }}
        >
          <img
            src={imageUrl}
            alt="Full View"
            style={{
              maxWidth: '100%',
              maxHeight: '80vh',
              objectFit: 'contain',
              marginBottom: '20px',
            }}
          />
          <Button
            variant="contained"
            sx={{
              backgroundColor: '#4caf50',
              '&:hover': {
                backgroundColor: '#45a049',
              },
              padding: '10px 20px',
              borderRadius: '25px',
            }}
            onClick={() => document.getElementById('profileImageInput').click()} 
          >
            Change Image
          </Button>
        </Box>
      </Modal>
    </Box>
  );
};

export default Profile;
