import { BrowserRouter, Routes, Route} from "react-router-dom"
import Login from "../Pages/Auth/Login/Login"
import Register from "../Pages/Auth/Register/Register"
import ForgotPassword from "../Pages/Auth/ForgotPassword/ForgotPassword"
import Home from '../Pages/Home/Home';
import Dashboard from "../Pages/Dashboard/Dashboard";
import PrivateRoute from "../Pages/Dashboard/PrivateRoute";
import Profile from "../Pages/Profile/Profile";
import Follower from "../Pages/Follower/Follower";
import Following from "../Pages/Following/Following";

const index = () => {
  return (
    <BrowserRouter>
      <Routes>
      <Route index element={<Home/>} />
      <Route path="/login" element={<Login />} />       
       <Route path="/register" element={<Register />} />
      <Route path="/forgot-pass" element={<ForgotPassword />} />
      <Route element={<PrivateRoute/>}>
          <Route path="/home" element={<Dashboard />} />
          <Route path="/profile" element={<Profile/>} />
          <Route path="/followers" element={<Follower/>} />
          <Route path="/following" element={<Following/>} />

        </Route>
      </Routes>
    </BrowserRouter>
  )
}

export default index
