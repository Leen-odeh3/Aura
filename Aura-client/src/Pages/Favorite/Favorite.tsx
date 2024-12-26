import { useState, useEffect } from 'react';
import axios from 'axios';
import './style.css'
import { Favorite as FavoriteIcon } from '@mui/icons-material';

const Favorite = () => {
  const [favoritePosts, setFavoritePosts] = useState<any[]>([]); 
  const [error, setError] = useState<string | null>(null);
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

        console.log('Response from API:', response.data); 
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
      await axios.delete(`/favorites/${postId}`, {
        headers: {
          'Authorization': `Bearer ${token}`,
        },
      });

      setFavoritePosts((prevPosts) => prevPosts.filter((post) => post.id !== postId));
      console.log(`Post ${postId} removed from favorites`);
    } catch (error) {
      setError('Error removing post from favorites');
      console.error('Error removing post from favorites', error);
    }
  };

  return (
    <div className="container">
       <p style={{fontWeight:"bold",fontSize:"25px",marginBottom:"20px"}}>My Saved Posts</p>
      {error && <p className="error-message">{error}</p>}
      {favoritePosts.length > 0 ? (
        favoritePosts.map((favorite) => (
          <div key={favorite.id} className="post">
            <div className="post-title">
              <span>{favorite.username}</span>
              <button
                onClick={() => handleRemoveFromFavorites(favorite.id.toString())}
                className="favorite-button"
              >
                <FavoriteIcon />
              </button>
            </div>
            <p className="post-content">{favorite.post.content}</p>
            {favorite.post.image && (
              <img src={favorite.post.image.imagePath} alt="Post image" className="post-image" />
            )}
          </div>
        ))
      ) : (
        <p>No favorite posts to display.</p>
      )}
    </div>
  );
};

export default Favorite;

