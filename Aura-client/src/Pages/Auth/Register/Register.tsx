import Box from '@mui/material/Box';
import Grid from '@mui/material/Grid';
import RegisterPage from '../../../../public/Images/RegisterPage.png';
import Form from './Form';

const Register = () => {
  return (
    <Box sx={{ flexGrow: 1, height: "100vh" }}>
      <Grid container sx={{ height: "100vh"}}>
        <Grid 
          xs={0} 
          lg={5} 
          sx={{
            display: "flex", 
            justifyContent:"end", 
            alignItems:"center"
          }}
        >
          <img 
            src={RegisterPage} 
            style={{
              width: "90%",
              height: "90%",
              objectFit: "contain"  
            }} 
            alt="RegisterPage"
          />
        </Grid>

        <Grid xs={12} lg={7}>
        <Form/>
        </Grid>
      </Grid>
    </Box>
  );
}

export default Register;
