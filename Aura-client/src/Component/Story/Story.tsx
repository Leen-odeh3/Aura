import { useState, useEffect } from 'react';
import axios from 'axios';  
import { Box, CircularProgress, IconButton, Input, Dialog, DialogActions, DialogContent, DialogTitle } from '@mui/material';  
import { AddCircle } from '@mui/icons-material'; 
import StoryData from '../../Interfaces/StoryData';

const Story = () => {
  const [stories, setStories] = useState<StoryData[]>([]);
  const [newImage, setNewImage] = useState<File | null>(null);
  const [loading, setLoading] = useState<boolean>(false);
  const [uploading, setUploading] = useState<boolean>(false);
  const [selectedStory, setSelectedStory] = useState<StoryData | null>(null);
  const [showFileInput, setShowFileInput] = useState<boolean>(false); // لتحديد ما إذا كانت نافذة رفع الصورة ستظهر

  useEffect(() => {
    const fetchStories = async () => {
      try {
        setLoading(true);
        const token = localStorage.getItem('token');
        
        if (token) {
          const response = await axios.get('http://localhost:5064/api/stories', {
            headers: {
              'Authorization': `Bearer ${token}`,
            },
          });
          setStories(response.data);
        }
      } catch (error) {
        console.error('Error fetching stories:', error);
      } finally {
        setLoading(false);
      }
    };

    fetchStories();
  }, []);

  const handleImageUpload = async () => {
    if (!newImage) {
      alert('Please select an image.');
      return;
    }

    const formData = new FormData();
    formData.append('Image', newImage);

    try {
      setUploading(true);
      const token = localStorage.getItem('token');
      if (token) {
        const response = await axios.post('http://localhost:5064/api/stories', formData, {
          headers: {
            'Authorization': `Bearer ${token}`,
            'Content-Type': 'multipart/form-data',
          },
        });
        setStories((prevStories) => [...prevStories, response.data]);
        setShowFileInput(false); // إخفاء نافذة رفع الصورة بعد رفع الصورة بنجاح
      }
    } catch (error) {
      console.error('Error uploading image:', error);
    } finally {
      setUploading(false);
    }
  };

  const handleStoryClick = (story: StoryData) => {
    setSelectedStory(story);
  };

  const handleCloseDialog = () => {
    setSelectedStory(null);
  };

  return (
    <Box sx={{ width: '100%', display: "flex", justifyContent: "start", alignItems: "center" }}>
      {/* أيقونة إضافة صورة عند الضغط عليها يتم إظهار نافذة رفع الصورة */}
      <IconButton onClick={() => setShowFileInput(true)} color="primary" sx={{ fontSize: 40 }}>
        <AddCircle />
      </IconButton>

      {uploading && (
        <Box sx={{ display: 'flex', justifyContent: 'center', marginBottom: 2 }}>
          <CircularProgress />
        </Box>
      )}

      <Box sx={{ display: 'flex', justifyContent: 'center', overflowX: 'auto' }}>
        {loading ? (
          <CircularProgress />
        ) : (
          stories.map((story) => (
            <Box
              key={story.id}
              sx={{
                width: 70,
                height: 70,
                margin: 1,
                borderRadius: '50%',
                overflow: 'hidden',
                boxShadow: 3,
                border: '2px solid #fff',
                display: 'flex',
                justifyContent: 'center',
                alignItems: 'center',
                backgroundImage: `url(${story.imagePath})`,
                backgroundSize: 'cover',
                backgroundPosition: 'center',
                cursor: 'pointer',
              }}
              onClick={() => handleStoryClick(story)}
            />
          ))
        )}
      </Box>

      {showFileInput && (
        <Box sx={{ display: 'flex', justifyContent: 'center', marginTop: 2 }}>
          <Input
            type="file"
            onChange={(e) => setNewImage(e.target.files ? e.target.files[0] : null)}
            sx={{ display: 'block' }}
            inputProps={{ accept: 'image/*' }}
          />
          <button onClick={handleImageUpload}>Upload Image</button> {/* زر لتحميل الصورة */}
        </Box>
      )}

      <Dialog open={Boolean(selectedStory)} onClose={handleCloseDialog}>
        <DialogTitle>Story</DialogTitle>
        <DialogContent>
          {selectedStory && (
            <img
              src={selectedStory.imagePath}
              alt="Story"
              style={{ width: '100%', height: 'auto' }}
            />
          )}
        </DialogContent>
        <DialogActions>
          <button onClick={handleCloseDialog}>Close</button>
        </DialogActions>
      </Dialog>
    </Box>
  );
};

export default Story;
