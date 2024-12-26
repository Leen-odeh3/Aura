import { useParams } from 'react-router-dom';
import axios from '../../api/axios';  // تأكد من أن axios مضبوط بشكل صحيح في ملف axios.js
import { Box, TextField, Button, Typography, CircularProgress, Snackbar, Alert } from '@mui/material';
import { useState, useEffect } from 'react';

const Chat = () => {
  const { userId } = useParams();  // الحصول على معرف المستخدم من الـ URL
  const [messages, setMessages] = useState<any[]>([]);  // لتخزين الرسائل
  const [newMessage, setNewMessage] = useState<string>('');  // لتخزين الرسالة الجديدة
  const [loading, setLoading] = useState<boolean>(true);  // لتحديد ما إذا كنا في حالة تحميل
  const [error, setError] = useState<string | null>(null);  // لتخزين الأخطاء
  const [snackbarOpen, setSnackbarOpen] = useState(false);  // للتحكم في عرض الـ Snackbar
  const [snackbarMessage, setSnackbarMessage] = useState('');
  const [snackbarSeverity, setSnackbarSeverity] = useState<'success' | 'error'>('success');

  // جلب الرسائل عند تحميل الصفحة
  useEffect(() => {
    const fetchMessages = async () => {
      const token = localStorage.getItem('token');
      if (!token) {
        setError('No authentication token found');
        return;
      }

      setLoading(true);

      try {
        const response = await axios.get(`/api/users/${userId}/recent-chats`, {
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

  // إرسال رسالة
  const handleSendMessage = async () => {
    if (newMessage.trim() === '') return;  // تأكد من أن الرسالة ليست فارغة

    const token = localStorage.getItem('token');
    if (!token) {
      setError('No authentication token found');
      return;
    }

    try {
      const response = await axios.post(
        `/api/users/${userId}/recent-chats/sendMessage`,  // المسار الصحيح هنا
        {
          userId: userId,    // إرسال الـ userId في الطلب
          message: newMessage,
        },
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );

      if (response.status === 200) {
        // إضافة الرسالة إلى الرسائل المعروضة
        setMessages((prevMessages) => [
          ...prevMessages,
          { sender: 'You', content: newMessage },
        ]);
        setNewMessage('');  // مسح محتوى مربع النص
        setSnackbarMessage('Message sent successfully');
        setSnackbarSeverity('success');
        setSnackbarOpen(true);  // فتح الـ Snackbar
      } else {
        setSnackbarMessage('Error sending message');
        setSnackbarSeverity('error');
        setSnackbarOpen(true);  // فتح الـ Snackbar
      }
    } catch (err) {
      setSnackbarMessage('Error sending message');
      setSnackbarSeverity('error');
      setSnackbarOpen(true);  // فتح الـ Snackbar
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

      {/* Snackbar for Success or Error */}
      <Snackbar open={snackbarOpen} autoHideDuration={3000} onClose={() => setSnackbarOpen(false)}>
        <Alert onClose={() => setSnackbarOpen(false)} severity={snackbarSeverity} sx={{ width: '100%' }}>
          {snackbarMessage}
        </Alert>
      </Snackbar>
    </Box>
  );
};

export default Chat;
