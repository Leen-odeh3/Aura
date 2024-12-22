import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom"
import Login from "../Pages/Auth/Login/Login"
import Register from "../Pages/Auth/Register/Register"
import ForgotPassword from "../Pages/Auth/ForgotPassword/ForgotPassword"


const index = () => {
  return (
    <BrowserRouter>
      <Routes>
      <Route index element={<Navigate to="/login" />} />
      <Route path="/login" element={<Login />} />       
       <Route path="/register" element={<Register />} />
      <Route path="/forgot-pass" element={<ForgotPassword />} />
      </Routes>
    </BrowserRouter>
  )
}

export default index
