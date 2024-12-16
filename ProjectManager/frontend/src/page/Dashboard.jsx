import React, { useState } from "react";
import { SlidebarLayout } from '../components/SlidebarLayout';
import { Outlet } from "react-router-dom";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { FaUserCircle } from 'react-icons/fa';  

const Dashboard = () => {
  const [showLogoutPanel, setShowLogoutPanel] = useState(false);

  const handleLogout = () => {
    localStorage.clear();
    toast.success("Đăng xuất thành công!");
    window.location.href = "/";
  };

  return (
    <div className="h-100">
      <div className="container-fluid d-flex h-100">
        <div className="sidebar bg-white shadow-sm">
          <SlidebarLayout />
        </div>
        <div className="main-layout flex-grow-1 d-flex flex-column">
          <header className="main-header bg-white p-3 shadow-sm d-flex justify-content-between align-items-center">
            <h3 className="m-0">Quản lý QR code / Tạo QR code</h3>
            <div className="user-panel position-relative">
              <FaUserCircle className="user-icon" size={30} />
              <div className="user-dropdown position-absolute end-0 top-100 mt-2">
                <button className="dropdown-item" onClick={handleLogout}>Đăng xuất</button>
              </div>
            </div>
          </header>

          <section className="main-body flex-grow-1 bg-light">
            <div className="body-content m-3 bg-white p-4 rounded">
            <Outlet />
            </div>
          </section>
        </div>
      </div>

      {/* Toast Notifications */}
      <ToastContainer
        position="bottom-right"
        autoClose={3000}
        hideProgressBar={false}
        newestOnTop
        closeOnClick
        rtl={false}
        pauseOnFocusLoss
        draggable
        pauseOnHover
      />
    </div>
  );
};

export default Dashboard;
