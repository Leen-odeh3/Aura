import { TextField, Button, Box, Typography} from '@mui/material';
import { useNavigate } from 'react-router-dom';

const ForgotPassword = () => {

  const navigate = useNavigate();

  return (
    <Box
      sx={{
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
        justifyContent: 'center',
        height: '100vh',
        padding: '20px',
        width: '450px',
        margin:"auto"
      }}
    >
      <Typography variant="h4" sx={{ marginBottom: '20px', fontWeight: 'bold' }}>
        Forgot Password
      </Typography>
      <Typography sx={{marginBottom:"10px",color:"gray",fontSize:"13px"}}>Note: A One-Time Passcode will be emailed to you, Please check your spam folder
       </Typography>
      <form style={{ width: '100%' }}>
        <TextField
          label="Email"
          name="email"
          type="email"
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
            borderRadius: '25px',
            '&:hover': {
              backgroundColor: '#f76c5e',
            },
          }}
        >
          Send Reset Link
        </Button>
      </form>

      <Box sx={{ marginTop: '20px' }}>
        <Typography variant="body2" sx={{ textAlign: 'center' }}>
          Remembered your password?{' '}
          <Button
            onClick={() => navigate('/login')}
            sx={{ color: '#fa8072', fontWeight: 'bold' }}
          >
            Login
          </Button>
        </Typography>
      </Box>
    </Box>
  );
};

export default ForgotPassword;