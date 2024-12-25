import { useEffect, useState } from 'react';
import axios from '../../api/axios';

const UserPosts = () => {
  const [posts, setPosts] = useState([]);
  const [error, setError] = useState<string>();
  const token = localStorage.getItem('token');
  const userId = localStorage.getItem('userId');

  useEffect(() => {
    const fetchUserPosts = async () => {
      if (!userId) {
        setError("User ID not found in localStorage");
        return;
      }

      try {
        const response = await axios.get(`/posts?loggedInUserId=${userId}`, {
          headers: {
            'Authorization': `Bearer ${token}`,
          },
        });
        setPosts(response.data);
      } catch (error) {
        setError("Error fetching posts");
        console.error("Error fetching posts", error);
      }
    };

    if (userId && token) {
      fetchUserPosts();
    }
  }, [token, userId]);

  return (
    <div>
      <style>{`
        .post {
          border: 1px solid #ddd;
          border-radius: 8px;
          margin-bottom: 20px;
          padding: 15px ;
          background-color: #f9f9f9;
          display: flex;
          flex-direction: column;
          gap: 10px;
          max-width: 600px;
          width: 100%; 
        }
        .post-title {
          font-size: 1rem;
          margin: 0;
          color: #666;
          }

        .post-content {
          font-size: 1.5rem;
          color: #666;
          color: #333;
        }
        .post-image {
       max-width: 600px;
       width: 100%;  
      border-radius: 8px;
      height:"400px"
        }
        .error-message {
          color: red;
          font-size: 1.2rem;
        }
      `}</style>

      <div className="container">
        {error && <p className="error-message">{error}</p>}
        {posts.length > 0 ? (
          <div>
            {posts.map(post => (
              <div key={post.id} className="post">
                <h3 className="post-title">{post.user.username}</h3>
                <p className="post-content">{post.content}</p>
                {post.image && <img src={post.image.imagePath} alt="Post image" className="post-image" />}
              </div>
            ))}
          </div>
        ) : (
          <p>No posts to display.</p>
        )}
      </div>
    </div>
  );
};

export default UserPosts;
