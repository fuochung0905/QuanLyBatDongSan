import React from "react";
import { BrowserRouter, useNavigate } from "react-router-dom";  // Sửa import
import LoginForm from "../components/LoginForm";
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import '../../public/css/login.css'

const Login = () => {
  const navigate = useNavigate();

  return (
    <div className="login-page">
      <LoginForm />
      <ToastContainer
        position="bottom-right"
        autoClose={1000}
        hideProgressBar={false}
        newestOnTop
        closeOnClick
        rtl={false}
        pauseOnFocusLoss
        draggable
        pauseOnHover
        onClose={() => navigate("/dashboard")}  
      />

    </div>
  );
};

export default Login;
