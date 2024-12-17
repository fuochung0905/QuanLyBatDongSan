import React, { useState } from "react";
import api from "../api/apiService"; 
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import "../css/LoginForm.css";

const LoginForm = () => {
  const [userName, setUserName] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const [loading, setLoading] = useState(false);

  const handleLogin = async (e) => {
    e.preventDefault();
    setLoading(true); 
    const credentials = {
      userName,
      password,
    };
    try {
      const response = await api.post("/taiKhoan/login", credentials); 
      console.log(response);
      if (response.data.success === true) {
        localStorage.clear();
        localStorage.setItem("userRole", response.data.result.taiKhoan.vaiTro);
        localStorage.setItem("authToken", response.data.result.taiKhoan.token); 
        localStorage.setItem("menu", JSON.stringify(response.data.result.menu));
        localStorage.setItem("nhomQuyen", JSON.stringify(response.data.result.nhomQuyen));
        localStorage.setItem("phanQuyen", JSON.stringify(response.data.result.phanQuyen));
        toast.success("Đăng nhập thành công");
      } else {
        toast.error(response.data.message); 
      }
    } catch (err) {
      if (err.response) {
        toast.error(err.response.data.message || "Lỗi server");
      } else if (err.request) {
        toast.error("Không thể kết nối đến máy chủ. Vui lòng thử lại.");
      } else {
        toast.error("Đã có lỗi xảy ra. Vui lòng thử lại.");
      }
    } finally {
      setLoading(false); 
    }
  };

  return (
    <div className="login-container">
      <div className="login-form-wrapper">
        <h2>Login</h2>
        <form onSubmit={handleLogin}>
          <div className="input-group">
            <label>Username:</label>
            <input
              type="text"
              value={userName}
              onChange={(e) => setUserName(e.target.value)}
              required
            />
          </div>
          <div className="input-group">
            <label>Password:</label>
            <input
              type="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              required
            />
          </div>
          {error && <p className="error">{error}</p>}
          <button type="submit" disabled={loading}>
            {loading ? "Logging in..." : "Login"}
          </button>
        </form>
      </div>
    </div>
  );
};

export default LoginForm;
