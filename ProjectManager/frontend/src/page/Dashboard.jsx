import React, { useState } from "react";
import { SlidebarLayout } from '../components/SlidebarLayout';
import { Outlet } from "react-router-dom";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { FaUserCircle } from 'react-icons/fa';  
import "../css/Dashboard.css";

const Dashboard = () => {
  const [showLogoutPanel, setShowLogoutPanel] = useState(false);

  const handleLogout = () => {
    localStorage.clear();
    window.location.href = "/";
  };

  return (
    <div className="h-100">
      <div className="container-fluid d-flex h-100">
        {/* Sidebar */}
        <div className="sidebar bg-white shadow-sm" style={{width: '250px'}}>
          <SlidebarLayout />
        </div>

        {/* Main Layout */}
        <div className="main-layout flex-grow-1 d-flex flex-column">
          <header className="main-header bg-white p-3 shadow-sm d-flex justify-content-between align-items-center">
            {/* Header Content */}
            <div className="d-flex align-items-center ml-auto">
              {/* User Icon */}
              <FaUserCircle className="user-icon" size={30} onClick={() => setShowLogoutPanel(true)} onMouseLeave={() => setShowLogoutPanel(false)} />
              {/* Logout Dropdown */}
              {showLogoutPanel && (
                <div className="user-dropdown position-absolute end-0 top-100 mt-2">
                  <div className="dropdown-menu">
                    <button className="dropdown-item"  onClick={handleLogout}>Đăng xuất</button>
                  </div>
                </div>
              )}
            </div>
          </header>

          {/* Main Body */}
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
