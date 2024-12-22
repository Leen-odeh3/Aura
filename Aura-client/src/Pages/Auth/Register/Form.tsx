import { Box, Button, TextField, Typography } from "@mui/material";
import { useNavigate } from "react-router-dom";

const Form = () => {
const navigate =useNavigate();

  return (
    <Box
      sx={{
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
        justifyContent: 'center',
        height: '100vh',
        padding: '10px',
        width: '70%',
        margin: 'auto',
      }}
    >
      <Typography variant="h4" sx={{ marginBottom: '20px', fontWeight: 'bold' }}>
        Create account
      </Typography>
      <form style={{ width: '100%' }}>
        <TextField
          label="First Name"
          name="FirstName"
          fullWidth
          required
          sx={{ marginBottom: '15px' }}
        />
        <TextField
          label="Last Name"
          name="LastName"
          fullWidth
          required
          sx={{ marginBottom: '15px' }}
        />
        <TextField
          label="Email"
          name="email"
          type="email"
          fullWidth
          required
          sx={{ marginBottom: '15px' }}
        />
        <TextField
          label="Password"
          name="password"
          type="password"
          fullWidth
          required
          sx={{ marginBottom: '20px' }}
        />
        <Button
          type="submit"
          fullWidth
          sx={{
            padding: '10px',
            backgroundColor: '#fa8072',
            color: 'white',
            borderRadius: '25px',  fontWeight:"bold",
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
        <Button onClick={() => navigate('/login')} sx={{ color: '#fa8072',fontWeight:"bold" }}>
          Login
        </Button>
      </Typography>
    </Box>
  );
};

export default Form;