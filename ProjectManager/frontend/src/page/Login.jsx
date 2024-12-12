import React from "react";
import { useNavigate } from "react-router-dom";
function Login  ()  {
  const navigate = useNavigate(); 

  const handleLogin = (e) => {
    e.preventDefault(); 
    const isAuthenticated = true; 

    if (isAuthenticated) {
      navigate("/dashboard"); 
    } else {
      alert("Thông tin đăng nhập không chính xác!");
    }
  };

  return (
    <form onSubmit={handleLogin}>
      {
     <div>
         <div className="mb-3">
            <label htmlFor="studentId" className="form-label">
              Tài khoản
            </label>
            <input
              type="text"
              id="studentId"
              className="form-control"
              placeholder="Nhập mã sinh viên"
            />
          </div>

          <div className="mb-3">
            <label htmlFor="password" className="form-label">
              Mật khẩu
            </label>
            <input
              type="password"
              id="password"
              className="form-control"
              placeholder="Nhập mật khẩu"
            />
          </div>

          <div className="mb-3">
            <label htmlFor="captcha" className="form-label">
              Mã xác nhận
            </label>
            <div className="d-flex align-items-center">
              <input
                type="text"
                id="captcha"
                className="form-control me-2"
                placeholder="Nhập mã xác nhận"
              />
              <img
                src="/captcha.png" // Replace with the actual captcha image path
                alt="Captcha"
                style={{ height: "38px" }}
              />
            </div>
          </div>

          <div className="form-check mb-3">
            <input
              type="checkbox"
              className="form-check-input"
              id="rememberMe"
            />
            <label className="form-check-label" htmlFor="rememberMe">
              Duy trì đăng nhập
            </label>
          </div>
     </div>
      }
      <button type="submit" className="btn btn-primary w-100">
        Đăng nhập
      </button>
    </form>
  );
};

export default Login;
