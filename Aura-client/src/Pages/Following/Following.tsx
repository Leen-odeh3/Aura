import { useState, useEffect } from 'react';
import axios from '../../api/axios';
import { Box, Typography, Grid, Button, Avatar, CircularProgress, TextField, Snackbar, Alert } from '@mui/material';
import Header from '../../Component/Header/Header';
import { useNavigate } from 'react-router-dom'; 
import MessageIcon from '@mui/icons-material/Message';

const Following = () => {
  const [users, setUsers] = useState<any[]>([]);
  const [filteredUsers, setFilteredUsers] = useState<any[]>([]);
  const [searchQuery, setSearchQuery] = useState<string>('');
  const [error, setError] = useState<string | null>(null);
  const [loading, setLoading] = useState<boolean>(false);
  const [snackbarOpen, setSnackbarOpen] = useState(false);
  const [snackbarMessage, setSnackbarMessage] = useState('');
  const [snackbarSeverity, setSnackbarSeverity] = useState<'success' | 'error'>('success');
  const navigate = useNavigate();

  useEffect(() => {
    const fetchUsers = async () => {
      const token = localStorage.getItem('token');

      if (!token) {
        setError('No authentication token found');
        return;
      }

      setLoading(true);

      try {
        const response = await axios.get(`/Users?pageNumber=1&pageSize=10`, {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        });

        if (response.status === 200) {
          if (response.data && response.data.users.length > 0) {
            setUsers(response.data.users);
            setFilteredUsers(response.data.users);
          } else {
            setError('No users found');
          }
        } else {
          setError('Failed to load users');
        }
      } catch (err) {
        setError('Error fetching users');
        console.error('Error fetching users:', err);
      } finally {
        setLoading(false);
      }
    };

    fetchUsers();
  }, []);

  const handleSearchChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const query = event.target.value.toLowerCase();
    setSearchQuery(query);

    if (query === '') {
      setFilteredUsers(users);
    } else {
      const filtered = users.filter(user => user.username.toLowerCase().includes(query));
      setFilteredUsers(filtered);
    }
  };

  const handleFollow = async (followedId: number) => {
    const token = localStorage.getItem('token');

    if (!token) {
      setError('No authentication token found');
      return;
    }

    setLoading(true);

    try {
      const response = await axios.post(
        '/Follow/follow',
        { followedId },
        {
          headers: {
            Authorization: `Bearer ${token}`,
            'Content-Type': 'application/json',
          },
        }
      );

      if (response.status === 200) {
        setSnackbarMessage('Successfully followed the user');
        setSnackbarSeverity('success');
      } else {
        setSnackbarMessage('Error following the user');
        setSnackbarSeverity('error');
      }
    } catch (err) {
      setSnackbarMessage('Error following the user');
      setSnackbarSeverity('error');
      console.error('Error following the user:', err);
    } finally {
      setLoading(false);
      setSnackbarOpen(true); 
    }
  };

  const handleMessage = (userId: number) => {
    navigate(`/chat/${userId}`);
  };

  return (
    <>
      <Header/>
      <Box sx={{ maxWidth: 1200, margin: 'auto', paddingTop: 4 }}>
        <Typography variant="h4" gutterBottom align="center" sx={{ fontWeight: 'bold' }}>
          Users You Can Follow
        </Typography>

        {error && <Typography color="error" align="center" sx={{ marginBottom: 2 }}>{error}</Typography>}
        {loading && <CircularProgress sx={{ display: 'block', margin: 'auto', marginTop: 4 }} />}

        <Box sx={{ marginBottom: 3, textAlign: 'center' }}>
          <TextField
            label="Search Users"
            variant="outlined"
            fullWidth
            value={searchQuery}
            onChange={handleSearchChange}
            sx={{ maxWidth: 400, margin: '20px auto' }}
          />
        </Box>

        <Grid container spacing={3}>
          {filteredUsers.length > 0 ? (
            filteredUsers.map((user) => (
              <Grid item xs={12} sm={6} md={3} key={user.id}>
                <Box sx={{
                  borderRadius: 4, padding: 2, border: "1px solid #e3e1e1",
                  display: 'flex', flexDirection: 'column', alignItems: 'center'
                }}>
                  <Box sx={{ textAlign: 'center' }}>
                    <Box sx={{ display: 'flex', flexDirection: 'column', alignItems: 'center', marginBottom: 2 }}>
                      <Avatar
                        src={user.image ? user.image.imagePath : '/default-avatar.jpg'}
                        sx={{ width: 90, height: 90, boxShadow: 3, marginBottom: 1 }}
                      />
                      <Typography variant="h6" sx={{ fontWeight: 'bold', marginBottom: 1 }}>
                        {user.username}
                      </Typography>
                      <Typography variant="body2" color="textSecondary" sx={{ marginBottom: 2, textAlign: 'center' }}>
                        {user.about || 'No bio available'}
                      </Typography>
                    </Box>
                    <Button
                      variant="contained"
                      sx={{
                        padding: '4px 24px',
                        width: '100%',
                        textTransform: 'none',
                        fontSize: '1rem',
                        marginBottom: '10px',
                        backgroundColor: 'var(--Primary)',
                      }}
                      onClick={() => handleFollow(user.id)}
                    >
                      Follow
                    </Button>
                    <Button
                      variant="outlined"
                      sx={{
                        width: '100%',
                        textTransform: 'none',
                        fontSize: '1rem',
                      }}
                      startIcon={<MessageIcon />}
                      onClick={() => handleMessage(user.id)}
                    >
                      Message
                    </Button>
                  </Box>
                </Box>
              </Grid>
            ))
          ) : (
            <Typography variant="h6" align="center">No users found.</Typography>
          )}
        </Grid>

        {/* Snackbar for Success or Error */}
        <Snackbar
          open={snackbarOpen}
          autoHideDuration={3000}
          onClose={() => setSnackbarOpen(false)}
        >
          <Alert onClose={() => setSnackbarOpen(false)} severity={snackbarSeverity} sx={{ width: '100%' }}>
            {snackbarMessage}
          </Alert>
        </Snackbar>
      </Box>
    </>
  );
};

export default Following;
