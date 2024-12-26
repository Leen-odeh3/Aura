import { useState } from 'react';
import { Button, Box, Dialog, DialogActions, DialogContent, DialogTitle } from '@mui/material';
import { ThumbUp as ThumbUpIcon, Replay as ReplayIcon, Share as ShareIcon } from '@mui/icons-material';
import axios from '../../api/axios'; 

interface IconProps {
  postId: number;
  liked: boolean;
  likes: number;
  onLike: (postId: number) => void;
  onUnlike: (postId: number) => void;
}

const Icon = ({ postId, liked, likes, onLike, onUnlike }: IconProps) => {
  const [openDialog, setOpenDialog] = useState(false);
  const [likers, setLikers] = useState<any[]>([]); 

  const token = localStorage.getItem('token'); 

  const handleLike = async () => {
    try {
      await axios.post(
        `/posts/${postId}/like`,
        {},
        {
          headers: {
            'Authorization': `Bearer ${token}`,
            'Content-Type': 'application/json',
          },
        }
      );
      onLike(postId); 
    } catch (error) {
      console.error('Error liking post', error);
    }
  };

  const handleUnlike = async () => {
    try {
      await axios.post(
        `/posts/${postId}/unlike`,
        {},
        {
          headers: {
            'Authorization': `Bearer ${token}`,
            'Content-Type': 'application/json',
          },
        }
      );
      onUnlike(postId);
    } catch (error) {
      console.error('Error unliking post', error);
    }
  };

  const fetchLikers = async () => {
    try {
      const response = await axios.get(`/Like`, {
        params: { postId },
        headers: {
          'Authorization': `Bearer ${token}`,
        },
      });
      setLikers(response.data);
      setOpenDialog(true);
    } catch (error) {
      console.error('Error fetching likers', error);
    }
  };

  return (
    <Box
      sx={{
        display: 'flex',
        justifyContent: 'space-between',
        padding: 1,
      }}
    >
      {/* Like Button */}
      <Button
        onClick={() => (liked ? handleUnlike() : handleLike())}
        sx={{
          display: 'flex',
          alignItems: 'center',
          padding: '8px 12px',
          border: '1px solid #ccc',
          cursor: 'pointer',
          border: "none",
          fontSize: '14px',
          textTransform: "capitalize",
          gap: 1,
          '&:hover': {
            backgroundColor: '#f0f0f0',
            borderColor: '#999',
          },
          color: '#59798E',
        }}
      >
        <ThumbUpIcon sx={{ width: 18, height: 18 }} /> {likes} Like
      </Button>

      {/* Repost Button */}
      <Button
        sx={{
          display: 'flex',
          alignItems: 'center',
          padding: '8px 12px',
          border: '1px solid #ccc',
          cursor: 'pointer',
          border: "none",
          fontSize: '14px',
          textTransform: "capitalize",
          gap: 1,
          '&:hover': {
            backgroundColor: '#f0f0f0',
            borderColor: '#999',
          },
          color: '#4CAF50',
        }}
      >
        <ReplayIcon sx={{ width: 18, height: 18 }} /> Repost
      </Button>

      <Button
        sx={{
          display: 'flex',
          alignItems: 'center',
          padding: '8px 12px',
          border: '1px solid #ccc',
          cursor: 'pointer',
          border: "none",
          fontSize: '14px',
          textTransform: "capitalize",
          gap: 1,
          '&:hover': {
            backgroundColor: '#f0f0f0',
            borderColor: '#999',
          },
          color: '#2196F3',
        }}
      >
        <ShareIcon sx={{ width: 18, height: 18 }} /> Share
      </Button>

      <Button
        onClick={fetchLikers}
        sx={{
          display: 'flex',
          alignItems: 'center',
          border: '1px solid #ccc',
          cursor: 'pointer',
          border: "none",
          fontSize: '14px',
          textTransform: "capitalize",
          gap: 1,
          '&:hover': {
            backgroundColor: '#f0f0f0',
            borderColor: '#999',
          },
          color: '#4CAF50',
        }}
      >
        See All
      </Button>

      <Dialog open={openDialog} onClose={() => setOpenDialog(false)}>
        <DialogTitle>People who liked this post</DialogTitle>
        <DialogContent>
          <div>
            {likers.map((liker: any) => (
              <div key={liker.userId} style={{ marginBottom: '8px' }}>
                <span>{liker.username}</span>
              </div>
            ))}
          </div>
        </DialogContent>

        <DialogActions>
          <Button onClick={() => setOpenDialog(false)} color="primary">
            Close
          </Button>
        </DialogActions>
      </Dialog>
    </Box>
  );
};

export default Icon;
