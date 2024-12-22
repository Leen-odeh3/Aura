import { BrowserRouter, Routes, Route} from "react-router-dom"
import Login from "../Pages/Auth/Login/Login"
import Register from "../Pages/Auth/Register/Register"
import ForgotPassword from "../Pages/Auth/ForgotPassword/ForgotPassword"
import Home from '../Pages/Home/Home';

const index = () => {
  return (
    <BrowserRouter>
      <Routes>
      <Route index element={<Home/>} />
      <Route path="/login" element={<Login />} />       
       <Route path="/register" element={<Register />} />
      <Route path="/forgot-pass" element={<ForgotPassword />} />
      </Routes>
    </BrowserRouter>
  )
}

export default index