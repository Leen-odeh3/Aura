import { useEffect, useState } from 'react';
import axios from '../../api/axios';
import ThumbUpIcon from '@mui/icons-material/ThumbUp'; 

const UserPosts = () => {
  const [posts, setPosts] = useState<any[]>([]);
  const [comments, setComments] = useState<any>({}); 
  const [error, setError] = useState<string>();
  const [comment, setComment] = useState<string>(''); 
  const [commentError, setCommentError] = useState<string>('');
  const token = localStorage.getItem('token');
  const userId = localStorage.getItem('userId');
  const [loadedComments, setLoadedComments] = useState<{[key: number]: number}>({});

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
        fetchCommentsForPosts(response.data);
      } catch (error) {
        setError("Error fetching posts");
        console.error("Error fetching posts", error);
      }
    };

    if (userId && token) {
      fetchUserPosts();
    }
  }, [token, userId]);

  const fetchCommentsForPosts = async (posts: any[]) => {
    for (const post of posts) {
      try {
        const response = await axios.get(`/posts/${post.id}/comments`, {
          headers: {
            'Authorization': `Bearer ${token}`,
          },
        });
        setComments((prevComments: any) => ({
          ...prevComments,
          [post.id]: response.data,
        }));
        setLoadedComments((prev: any) => ({
          ...prev,
          [post.id]: 2, 
        }));
      } catch (error) {
        console.error(`Error fetching comments for post ${post.id}`, error);
      }
    }
  };

  const handleLike = async (postId: number) => {
    try {
      const response = await axios.post(
        `/posts/${postId}/like`, 
        {},
        {
          headers: {
            'Authorization': `Bearer ${token}`,
            'Content-Type': 'application/json',
          },
        }
      );
      setPosts((prevPosts) =>
        prevPosts.map((post) =>
          post.id === postId ? { ...post, likes: post.likes + 1 } : post
        )
      );
    } catch (error) {
      console.error('Error liking post', error);
    }
  };

  const handleUnlike = async (postId: number) => {
    try {
      const response = await axios.post(
        `/posts/${postId}/unlike`,
        {},
        {
          headers: {
            'Authorization': `Bearer ${token}`,
            'Content-Type': 'application/json',
          },
        }
      );
      setPosts((prevPosts) =>
        prevPosts.map((post) =>
          post.id === postId ? { ...post, likes: post.likes - 1 } : post
        )
      );
    } catch (error) {
      console.error('Error unliking post', error);
    }
  };

  const handleCommentSubmit = async (postId: number) => {
    if (!comment.trim()) {
      setCommentError('Comment cannot be empty');
      return;
    }

    try {
      const response = await axios.post(
        `/posts/${postId}/comments`,
        { content: comment },
        {
          headers: {
            'Authorization': `Bearer ${token}`,
            'Content-Type': 'application/json',
          },
        }
      );

      setComment('');
      setCommentError('');
      fetchCommentsForPosts(posts);
    } catch (error) {
      setCommentError('Error posting comment');
      console.error('Error posting comment', error);
    }
  };

  const handleSeeMore = (postId: number) => {
    const currentLoaded = loadedComments[postId] || 2;
    const nextLoadCount = currentLoaded + 2;
    setLoadedComments((prev: any) => ({
      ...prev,
      [postId]: nextLoadCount, 
    }));
  };

  const getCommentsToDisplay = (postId: number) => {
    const allComments = comments[postId] || [];
    const loadedCount = loadedComments[postId] || 2;
    return allComments.slice(0, loadedCount);
  };

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
          height: 400px;
        }
        .error-message {
          color: red;
          font-size: 1.2rem;
        }
        .comment-input {
          width: 100%;
          padding: 8px;
          margin-bottom: 10px;
        }
        .comment-button {
          background-color: #4CAF50;
          color: white;
          padding: 10px 15px;
          border: none;
          cursor: pointer;
          border-radius: 5px;
        }
        .comment-error {
          color: red;
          font-size: 1rem;
        }
        .comment-list {
          margin-top: 15px;
          width:"100px"
        }
        .comment {
          border: 1px solid #ddd;
          border-radius: 5px;
          margin-bottom: 10px;
          padding: 10px;
          background-color: #f1f1f1;
        }
        .comment-username {
          font-weight: bold;
          color: #333;
        }
        .comment-content {
          color: #555;
        }
        .see-more-button {
          color:#FF6F61;
          padding: 10px;
          border: none;
          cursor: pointer;
          border-radius: 5px;
          margin-top: 10px;
        }
        .like-button {
          background-color: #FF6F61;
          color: white;
          padding: 8px;
          border: none;
          cursor: pointer;
          border-radius: 5px;
          margin-top: 10px;
        }
      `}</style>

      <div className="container">
        {error && <p className="error-message">{error}</p>}
        {posts.length > 0 ? (
          <div>
            {posts.map((post) => (
              <div key={post.id} className="post">
                <h3 className="post-title">{post.user?.username}</h3>
                <p className="post-content">{post.content}</p>
                {post.image && <img src={post.image.imagePath} alt="Post image" className="post-image" />}

                <button
                  onClick={() => (post.liked ? handleUnlike(post.id) : handleLike(post.id))}
                  className="like-button"
                >
                  <ThumbUpIcon /> {post.likes}
                </button>

                {/* Comment form */}
                <div>
                  <textarea
                    value={comment}
                    onChange={(e) => setComment(e.target.value)}
                    className="comment-input"
                    placeholder="Write a comment..."
                  />
                  <button
                    onClick={() => handleCommentSubmit(post.id)}
                    className="comment-button"
                  >
                    Post Comment
                  </button>
                  {commentError && <p className="comment-error">{commentError}</p>}
                </div>

                <div className="comment-list">
                  {getCommentsToDisplay(post.id).map((comment: any) => (
                    <div key={comment.id} className="comment">
                      <p className="comment-username">{comment.username}</p>
                      <p className="comment-content">{comment.content}</p>
                    </div>
                  ))}

                  {comments[post.id]?.length > (loadedComments[post.id] || 2) && (
                    <button onClick={() => handleSeeMore(post.id)} className="see-more-button">
                      See More
                    </button>
                  )}
                </div>
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
