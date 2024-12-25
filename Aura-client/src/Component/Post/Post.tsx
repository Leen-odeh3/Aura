import { useState } from "react";
import TagIcon from '@mui/icons-material/Tag';
import EmojiEmotionsIcon from '@mui/icons-material/EmojiEmotions';
import LocationOnIcon from '@mui/icons-material/LocationOn';
import {
  Box,
  Avatar,
  TextField,
  Button,
  IconButton,
  Typography,
} from "@mui/material";
import ImageIcon from "@mui/icons-material/Image";
import axios from "../../api/axios"; 

const Post = () => {
  const [postText, setPostText] = useState("");
  const [image, setImage] = useState(null); 
  const [imagePreview, setImagePreview] = useState(null); 
  
  const token = localStorage.getItem("token");

  const handleImageChange = (e) => {
    const file = e.target.files[0];
    if (file) {
      setImage(file);
      const previewUrl = URL.createObjectURL(file); 
      setImagePreview(previewUrl);
    }
  };

  const handlePost = async () => {
    const formData = new FormData();
    formData.append("Content", postText);
    formData.append("IsPrivate", false); 
    if (image) {
      formData.append("Image", image); 
    }

    try {
      const response = await axios.post("/posts", formData, {
        headers: {
          "Content-Type": "multipart/form-data", 
          Authorization: `Bearer ${token}`,
        },
      });
      console.log("Post Created:", response.data);
      setPostText(""); 
      setImage(null); 
      setImagePreview(null); 
    } catch (error) {
      console.error("Error creating post:", error);
    }
  };

  return (
    <Box
      sx={{
        display: "flex",
        flexDirection: "column",
        gap: 2,
        p: 2,
        maxWidth: "600px",
        borderRadius: "15px",
        boxShadow: "0px 4px 10px rgba(0, 0, 0, 0.1)",
        backgroundColor: "#fff",
      }}
    >
      {/* Header */}
      <Box sx={{ display: "flex", alignItems: "center", gap: 2 }}>
        <Avatar src="/path/to/avatar.jpg" alt="" />
        <TextField
          placeholder="What's on your mind?"
          variant="standard"
          fullWidth
          value={postText}
          onChange={(e) => setPostText(e.target.value)}
          InputProps={{
            disableUnderline: true,
          }}
          sx={{ bgcolor: "#f5f5f5", borderRadius: "10px", p: 1 }}
        />
      </Box>

      {imagePreview && (
        <Box
          sx={{
            mt: 2,
            maxWidth: "100%",
            maxHeight: "300px",
            overflow: "hidden",
            borderRadius: "10px",
            boxShadow: "0px 2px 5px rgba(0, 0, 0, 0.1)",
          }}
        >
          <img
            src={imagePreview}
            alt="Preview"
            style={{
              width: "100%",
              height: "auto",
              objectFit: "cover",
              borderRadius: "10px",
            }}
          />
        </Box>
      )}

      {/* Action Buttons */}
      <Box sx={{ display: "flex", gap: 2, alignItems: "center", mt: 2 }}>
        <input
          type="file"
          onChange={handleImageChange}
          style={{ display: "none" }}
          id="image-upload"
        />
        <label htmlFor="image-upload">
          <IconButton component="span">
            <ImageIcon sx={{ color: "#f76c5e" }} />
            <Typography variant="body2" sx={{ ml: 1 }}>
              Photo or Video
            </Typography>
          </IconButton>
          <IconButton>
          <EmojiEmotionsIcon sx={{ color: "#ffb400" }} />
          <Typography variant="body2" sx={{ ml: 1 }}>
            Feeling
          </Typography>
        </IconButton>

        <IconButton>
          <TagIcon sx={{ color: "#3b5998" }} />
          <Typography variant="body2" sx={{ ml: 1 }}>
            Hashtag
          </Typography>
        </IconButton>
        
        <IconButton>
          <LocationOnIcon sx={{ color: "#b06ec4" }} />
          <Typography variant="body2" sx={{ ml: 1 }}>
            Location
          </Typography>
        </IconButton>
        
        </label>
        <Button
          variant="contained"
          sx={{
            bgcolor: "#4caf50",
            textTransform: "capitalize",
            borderRadius: "20px",
            "&:hover": { bgcolor: "#45a049" },
          }}
          onClick={handlePost}
        >
          Share
        </Button>
      </Box>
    </Box>
  );
};

export default Post;
