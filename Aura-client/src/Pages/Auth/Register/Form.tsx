import { Box, Button, TextField, Typography } from "@mui/material";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import UserData from "../../../Interfaces/UserData";
import axios from "../../../api/axios";

const Form = () => {
  const navigate = useNavigate();

  
  const USER_REGEX = /^[A-z][A-z0-9_]{4,17}$/;
  const PWD_REGEX = /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%]).{8,24}$/;
  const REGISTER_URL = '/registration';

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

    if (!USER_REGEX.test(userData.username)) {
      setError('Username must start with a letter and be between 5-18 characters.');
      return;
    }

    if (!PWD_REGEX.test(userData.password)) {
      setError('Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.');
      return;
    }

    try {
      const response = await axios.post(REGISTER_URL, {
        username: userData.username,
        password: userData.password
      });

      console.log('Registration successful:', response.data);
      navigate('/login'); 
    } catch (error) {
      console.error('Registration failed:', error);
      setError('There was an issue with the registration. Please try again.');
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
        padding: '10px',
        width: '60%',
        margin: 'auto',
      }}
    >
      <Typography variant="h4" sx={{ marginBottom: '20px', fontWeight: 'bold' }}>
        Create account
      </Typography>
      <form style={{ width: '100%' }} onSubmit={handleSubmit}>
        <TextField
          label="Usename"
          name="username"
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
            borderRadius: '25px', fontWeight: "bold",
            '&:hover': {
              backgroundColor: '#f76c5e'

            },
          }}
        >
          Register
        </Button>
      </form>
      <Typography sx={{ marginTop: '20px', textAlign: 'center' }}>
        Already have an account?{' '}
        <Button onClick={() => navigate('/login')} sx={{ color: '#fa8072', fontWeight: "bold" }}>
          Login
        </Button>
      </Typography>
    </Box>
  );
};

export default Form;