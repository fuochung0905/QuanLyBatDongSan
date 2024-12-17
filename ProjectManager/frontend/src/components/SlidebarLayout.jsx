import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';

export const SlidebarLayout = () => {
  const [menuList, setMenuList] = useState([]);
  const [headerList, setHeaderList] = useState([]);
  const [user, setUser] = useState(null); 
  const [userRole, setUserRole] = useState(null);  
  const [openMenu, setOpenMenu] = useState(null);  

  useEffect(() => {
    const cachedMenu = JSON.parse(localStorage.getItem('menu')) || [];
    const cachedGroupRole = JSON.parse(localStorage.getItem('nhomQuyen')) || [];
    const cachedUser = JSON.parse(localStorage.getItem('user')) || null;
    const storedUserRole = localStorage.getItem("userRole");  
    setMenuList(cachedMenu);
    setHeaderList(cachedGroupRole);
    setUser(cachedUser);
    setUserRole(storedUserRole);  
  }, []);

  const toggleMenu = (menuId) => {
    setOpenMenu(openMenu === menuId ? null : menuId);  
  };

  return (
    <div className="sidebar">
      <nav className="mt-2">
        <ul className="nav nav-pills nav-sidebar flex-column" data-widget="treeview" role="menu" data-accordion="false">
          {headerList.map((header) => (
            <li className="nav-item" key={header.sort}>
              <a
                className="nav-link"
                href="#"
                onClick={() => toggleMenu(header.sort)} 
                data-bs-toggle="collapse"
                aria-expanded={openMenu === header.sort ? 'true' : 'false'}
                aria-controls={`ui-basic-${header.sort}`}
              >
                <i className="icon-layout menu-icon"></i>
                <span className="menu-title">{header.tenGoi.toUpperCase()}</span>
                <i className={`menu-arrow ${openMenu === header.sort ? 'open' : ''}`}></i>
              </a>
              <div className={`collapse ${openMenu === header.sort ? 'show' : ''}`} id={`ui-basic-${header.sort}`}>
                <ul className="nav flex-column sub-menu">
                  {menuList
                    .filter((menu) => menu.nhomQuyenId === header.id)  
                    .sort((a, b) => a.sort - b.sort)  
                    .map((menu, index) => {
                      const key = `${menu.action || 'defaultAction'}_${menu.controllerName || 'defaultController'}_${index}`;
                      return (
                        <li className="nav-item" id={`mn${menu.action}_${menu.controllerName}`} key={key}>
                        <Link to={`/dashboard/${menu.controllerName}`} className="nav-link">
                          {menu.tenGoi}
                        </Link>
                        </li>
                      );
                    })}
                </ul>
              </div>
            </li>
          ))}
          {userRole === 'Admin' && (
            <li className="nav-item">
              <a className="nav-link" data-bs-toggle="collapse" href="#admin" aria-expanded="false" aria-controls="admin">
                <i className="icon-layout menu-icon"></i>
                <span className="menu-title">ADMINISTRATOR</span>
                <i className={`menu-arrow ${openMenu === 'admin' ? 'open' : ''}`}></i>
              </a>
              <div className={`collapse ${openMenu === 'admin' ? 'show' : ''}`} id="admin">
                <ul className="nav flex-column sub-menu">
                  <li className="nav-item">
                    <Link to="/dashboard/menu" className="nav-link">
                      Menu
                    </Link>
                  </li>
                  <li className="nav-item">
                    <Link to="/dashboard/nhomQuyen" className="nav-link">
                      Nhóm quyền
                    </Link>
                  </li>
                </ul>
              </div>
            </li>
          )}
        </ul>
      </nav>
    </div>
  );
};
