import { TextField, Button, Box, Typography } from '@mui/material';
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import UserData from '../../../Interfaces/UserData';
import axios from '../../../api/axios';

const Login = () => {

  const navigate = useNavigate();

  const [userData, setUserData] = useState<UserData>({
    username: '',
    password: ''
  });


  const [error, setError] = useState<string>('');

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setUserData({
      ...userData,
      [name]: value
    });
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    try {
      const response = await axios.post('/login', {
        username: userData.username,
        password: userData.password
      });

      console.log('Login successful:', response.data);
      localStorage.setItem('token', response.data.token); 
      navigate('/home'); 
    } catch (error) {
      console.error('Login failed:', error);
      setError('Invalid username or password');
    }
  };


  return (
    <Box
    sx={{
      display: 'flex',
      flexDirection: 'column',
      alignItems: 'center',
      justifyContent: 'center',
      height: '100vh',
      padding: '20px',
      width: '400px',
      margin: 'auto'
    }}
  >
    <Typography variant="h4" sx={{ marginBottom: '20px', fontWeight: 'bold' }}>
      Login
    </Typography>

    <form style={{ width: '100%' }} onSubmit={handleSubmit}>
      <TextField
        label="Username"
        name="username"
        type="text"
        value={userData.username}
        onChange={handleInputChange}
        fullWidth
        required
        sx={{ marginBottom: '15px' }}
      />
      <TextField
        label="Password"
        name="password"
        type="password"
        value={userData.password}
        onChange={handleInputChange}
        fullWidth
        required
        sx={{ marginBottom: '20px' }}
      />
      {error && (
        <Typography color="error" sx={{ marginBottom: '10px' }}>
          {error}
        </Typography>
      )}
      <Button
        type="submit"
        fullWidth
        sx={{
          padding: '10px',
          backgroundColor: '#fa8072',
          color: 'white',
          borderRadius: '25px',
          '&:hover': {
            backgroundColor: '#f76c5e',
          },
        }}
      >
        Login
      </Button>
    </form>

    <Box sx={{ marginTop: '20px' }}>
      <Typography variant="body2" sx={{ textAlign: 'center' }}>
        Don't have an account?{' '}
        <Button
          onClick={() => navigate('/register')}
          sx={{ color: '#fa8072', fontWeight: 'bold' }}
        >
          Register
        </Button>
      </Typography>
      <Typography variant="body2" sx={{ textAlign: 'center', marginTop: '10px' }}>
        Forgot your password?{' '}
        <Button
          onClick={() => navigate('/forgot-pass')}
          sx={{ color: '#fa8072', fontWeight: 'bold' }}
        >
          Recover Password
        </Button>
      </Typography>
    </Box>

    {/* Decorative elements */}
    <Box
      sx={{
        position: 'absolute',
        height: '25%',
        width: '12%',
        top: 'calc(36% - 40%)',
        right: '90%',
        backgroundColor: '#0d1e57',
        overflow: 'hidden',
        borderRadius: '50%',
      }}
    ></Box>
    <Box
      sx={{
        position: 'absolute',
        height: '20%',
        width: '10%',
        top: 'calc(40% - 45%)',
        right: '94%',
        backgroundColor: '#f7caba',
        overflow: 'hidden',
        borderRadius: '50%',
      }}
    ></Box>
    <Box
      sx={{
        position: 'absolute',
        height: '10%',
        width: '5%',
        top: 'calc(100% - 20%)',
        right: '90%',
        backgroundColor: '#f7caba',
        overflow: 'hidden',
        borderRadius: '50%',
      }}
    ></Box>

    <Box
      sx={{
        position: 'absolute',
        height: '20%',
        width: '10%',
        top: 'calc(100% - 50%)',
        right: '10%',
        backgroundColor: '#daedad',
        overflow: 'hidden',
        borderRadius: '50%',
      }}
    ></Box>

    <Box
      sx={{
        position: 'absolute',
        height: '15%',
        width: '7%',
        top: 'calc(100% - 70%)',
        left: '87%',
        backgroundColor: '#add0ed',
        overflow: 'hidden',
        borderRadius: '50%',
      }}
    ></Box>
  </Box>
);
};
export default Login;