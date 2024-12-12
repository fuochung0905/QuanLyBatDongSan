import React, { useState } from "react";
import { Link, Outlet } from "react-router-dom";

const Dashboard = () => {
  const [projectOpen, setProjectOpen] = useState(false);
  const [profileOpen, setProfileOpen] = useState(false);
  const [systemOpen, setSystemOpen] = useState(false);

  const toggleSubMenu = (menu) => {
    if (menu === "project") setProjectOpen(!projectOpen);
    if (menu === "profile") setProfileOpen(!profileOpen);
    if (menu === "system") setSystemOpen(!systemOpen);
  };

  return (
    <div className="container-fluid d-flex h-100">
      <div className="sidebar bg-white shadow-sm">
        <nav className="menu">
          <ul className="list-unstyled p-0 m-0">
            <li>
              <Link to="overview" className="nav-link">
                Tổng quan
              </Link>
            </li>

            {/* Quản lí dự án with sub-menu */}
            <li>
              <a
                className="nav-link"
                role="button"
                onClick={() => toggleSubMenu("project")}
              >
                Quản lí dự án
                <i
                  className={`fas ${
                    projectOpen ? "fa-chevron-up" : "fa-chevron-down"
                  } ms-2`}
                ></i>
              </a>
              <div className={`collapse ${projectOpen ? "show" : ""}`}>
                <ul className="list-unstyled ps-3">
                  <li>
                    <Link to="projects" className="nav-link">
                      Danh sách dự án
                    </Link>
                  </li>
                  <li>
                    <Link to="tasks" className="nav-link">
                      Quản lý nhiệm vụ
                    </Link>
                  </li>
                </ul>
              </div>
            </li>

            {/* Hồ sơ cá nhân with sub-menu */}
            <li>
              <a
                className="nav-link"
                role="button"
                onClick={() => toggleSubMenu("profile")}
              >
                Hồ sơ cá nhân
                <i
                  className={`fas ${
                    profileOpen ? "fa-chevron-up" : "fa-chevron-down"
                  } ms-2`}
                ></i>
              </a>
              <div className={`collapse ${profileOpen ? "show" : ""}`}>
                <ul className="list-unstyled ps-3">
                  <li>
                    <Link to="personalInfo" className="nav-link">
                      Thông tin cá nhân
                    </Link>
                  </li>
                  <li>
                    <Link to="settings" className="nav-link">
                      Cài đặt
                    </Link>
                  </li>
                </ul>
              </div>
            </li>

            {/* Hệ thống with sub-menu */}
            <li>
              <a
                className="nav-link"
                role="button"
                onClick={() => toggleSubMenu("system")}
              >
                Hệ thống
                <i
                  className={`fas ${
                    systemOpen ? "fa-chevron-up" : "fa-chevron-down"
                  } ms-2`}
                ></i>
              </a>
              <div className={`collapse ${systemOpen ? "show" : ""}`}>
                <ul className="list-unstyled ps-3">
                  <li>
                    <Link to="settings" className="nav-link">
                      Cài đặt hệ thống
                    </Link>
                  </li>
                  <li>
                    <Link to="logs" className="nav-link">
                      Nhật ký hoạt động
                    </Link>
                  </li>
                </ul>
              </div>
            </li>
          </ul>
        </nav>
      </div>

      <div className="main-layout flex-grow-1 d-flex flex-column">
        <header className="main-header bg-white p-3 shadow-sm">
          <h3 className="m-0">Quản lý QR code / Tạo QR code</h3>
        </header>

        <section className="main-body flex-grow-1 bg-light">
          <div className="body-content m-3 bg-white p-4 rounded">
            <Outlet />
          </div>
        </section>
      </div>
    </div>
  );
};

export default Dashboard;
