import { useParams } from 'react-router-dom';
import axios from '../../api/axios'; 
import { Box, TextField, Button, Typography, CircularProgress, Snackbar, Alert } from '@mui/material';
import { useState, useEffect } from 'react';

const Chat = () => {
  const { userId } = useParams(); 
  const [messages, setMessages] = useState<any[]>([]); 
  const [newMessage, setNewMessage] = useState<string>('');  
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);  
  const [snackbarOpen, setSnackbarOpen] = useState(false);  
  const [snackbarMessage, setSnackbarMessage] = useState('');
  const [snackbarSeverity, setSnackbarSeverity] = useState<'success' | 'error'>('success');

  useEffect(() => {
    const fetchMessages = async () => {
      const token = localStorage.getItem('token');
      if (!token) {
        setError('No authentication token found');
        return;
      }

      setLoading(true);

      try {
        const response = await axios.get(`/users/${userId}/recent-chats`, {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        });

        if (response.status === 200) {
          setMessages(response.data.messages);
        } else {
          setError('Failed to load messages');
        }
      } catch (err) {
        setError('Error fetching messages');
      } finally {
        setLoading(false);
      }
    };

    fetchMessages();
  }, [userId]);

  const handleSendMessage = async () => {
    if (newMessage.trim() === '') return; 

    const token = localStorage.getItem('token');
    if (!token) {
      setError('No authentication token found');
      return;
    }

    try {
      const response = await axios.post(
        `/users/${userId}/recent-chats/sendMessage`,  
        {
          userId: userId, 
          message: newMessage,
        },
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );

      if (response.status === 200) {
        setMessages((prevMessages) => [
          ...prevMessages,
          { sender: 'You', content: newMessage },
        ]);
        setNewMessage(''); 
        setSnackbarMessage('Message sent successfully');
        setSnackbarSeverity('success');
        setSnackbarOpen(true);  
      } else {
        setSnackbarMessage('Error sending message');
        setSnackbarSeverity('error');
        setSnackbarOpen(true);  
      }
    } catch (err) {
      setSnackbarMessage('Error sending message');
      setSnackbarSeverity('error');
      setSnackbarOpen(true);  
    }
  };

  return (
    <Box sx={{ maxWidth: 600, margin: 'auto', paddingTop: 4 }}>
      <Typography variant="h4" gutterBottom align="center" sx={{ fontWeight: 'bold' }}>
        Chat with User {userId}
      </Typography>

      {loading ? (
        <CircularProgress sx={{ display: 'block', margin: 'auto' }} />
      ) : (
        <Box>
          <Box sx={{ height: 400, overflowY: 'auto', marginBottom: 2 }}>
            {messages.length === 0 ? (
              <Typography variant="body1" align="center">No messages yet.</Typography>
            ) : (
              messages.map((message, index) => (
                <Box key={index} sx={{ marginBottom: 2, textAlign: message.sender === 'You' ? 'right' : 'left' }}>
                  <Typography variant="body2" color="textSecondary">{message.sender}</Typography>
                  <Typography variant="body1">{message.content}</Typography>
                </Box>
              ))
            )}
          </Box>

          <Box sx={{ display: 'flex', alignItems: 'center' }}>
            <TextField
              label="Type a message"
              variant="outlined"
              fullWidth
              value={newMessage}
              onChange={(e) => setNewMessage(e.target.value)}
              sx={{ marginRight: 2 }}
            />
            <Button variant="contained" onClick={handleSendMessage}>Send</Button>
          </Box>
        </Box>
      )}

      <Snackbar open={snackbarOpen} autoHideDuration={3000} onClose={() => setSnackbarOpen(false)}>
        <Alert onClose={() => setSnackbarOpen(false)} severity={snackbarSeverity} sx={{ width: '100%' }}>
          {snackbarMessage}
        </Alert>
      </Snackbar>
    </Box>
  );
};

export default Chat;
