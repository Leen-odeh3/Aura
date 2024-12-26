import { useState, useEffect } from 'react';
import axios from 'axios';
import { Favorite as FavoriteIcon } from '@mui/icons-material';
import { Snackbar, Alert } from '@mui/material';

const Favorite = () => {
  const [favoritePosts, setFavoritePosts] = useState<any[]>([]);
  const [error, setError] = useState<string | null>(null);
  const [snackbarOpen, setSnackbarOpen] = useState(false);
  const [snackbarMessage, setSnackbarMessage] = useState('');
  const token = localStorage.getItem('token');

  useEffect(() => {
    const fetchFavoritePosts = async () => {
      if (!token) {
        setError('User not authenticated');
        return;
      }

      try {
        const response = await axios.get('http://localhost:5064/api/Favorite/userFavorites', {
          headers: {
            'Authorization': `Bearer ${token}`,
          },
        });

        if (Array.isArray(response.data)) {
          setFavoritePosts(response.data);
        } else {
          setError('Received data is not in expected format');
        }
      } catch (error) {
        setError('Error fetching favorite posts');
        console.error('Error fetching favorite posts', error);
      }
    };

    fetchFavoritePosts();
  }, [token]);

  const handleRemoveFromFavorites = async (postId: string) => {
    try {
      const response = await axios.delete(`http://localhost:5064/api/Favorite/${postId}`, {
        headers: {
          'Authorization': `Bearer ${token}`,
        },
      });

      setFavoritePosts((prevPosts) => prevPosts.filter((post) => post.id !== postId));

      setSnackbarMessage('Post removed from favorites');
      setSnackbarOpen(true);
    } catch (error) {
      setError('Error removing post from favorites');
      console.error('Error removing post from favorites', error);
    }
  };

  return (
    <div style={{
      display: 'flex',
      flexDirection: 'column',
      alignItems: 'center',
      padding: '20px',
    }}>
      <p style={{ fontWeight: 'bold', fontSize: '25px', marginBottom: '20px' }}>My Saved Posts</p>
      {error && <p style={{ color: 'red', fontWeight: 'bold' }}>{error}</p>}
      {favoritePosts.length > 0 ? (
        favoritePosts.map((favorite) => (
          <div key={favorite.id} style={{
            width: '50%',
            margin: 'auto',
            border: '1px solid rgb(212, 211, 211)',
            borderRadius: '25px',
            marginTop: '20px',
            marginBottom: '30px',
            padding: '20px',
          }}>
            <div style={{
              display: 'flex',
              justifyContent: 'space-between',
              alignItems: 'center',
            }}>
              <span>{favorite.username}</span>
              <button
                onClick={() => handleRemoveFromFavorites(favorite.id.toString())}
                style={{
                  backgroundColor: 'white',
                  border: 'none',
                  color: '#0566ab',
                  cursor: 'pointer',
                }}
              >
                <FavoriteIcon />
              </button>
            </div>
            <p style={{ marginTop: '10px', color: '#555' }}>{favorite.post.content}</p>
            {favorite.post.image && (
              <img
                src={favorite.post.image.imagePath}
                alt="Post image"
                style={{
                  width: '100%',
                  height: 'auto',
                  marginTop: '10px',
                  margin: 'auto',
                  borderRadius: '5px',
                }}
              />
            )}
          </div>
        ))
      ) : (
        <p>No saved posts to display.</p>
      )}

      <Snackbar
        open={snackbarOpen}
        autoHideDuration={3000}
        onClose={() => setSnackbarOpen(false)}
      >
        <Alert onClose={() => setSnackbarOpen(false)} severity="success">
          {snackbarMessage}
        </Alert>
      </Snackbar>
    </div>
  );
};

export default Favorite;
