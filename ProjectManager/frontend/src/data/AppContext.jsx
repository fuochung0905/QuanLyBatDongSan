import React, { createContext, useState, useEffect } from 'react';

export const AppContext = createContext();

export const AppProvider = ({ children }) => {
  const [menuList, setMenuList] = useState([]);
  const [headerList, setHeaderList] = useState([]);
  const [user, setUser] = useState(null);  
  const [userRole, setUserRole] = useState(null);

  useEffect(() => {
    const cachedMenu = JSON.parse(localStorage.getItem('menu')) || [];
    const cachedGroupRole = JSON.parse(localStorage.getItem('nhomQuyen')) || [];
    const cachedUser = JSON.parse(localStorage.getItem('user')) || null;
    const storedUserRole = localStorage.getItem('userRole');

    // Set các giá trị vào state
    setMenuList(cachedMenu);
    setHeaderList(cachedGroupRole);
    setUser(cachedUser);
    setUserRole(storedUserRole);

    // Kiểm tra dữ liệu trong console.log
    console.log("Cached Menu: ", cachedMenu);
    console.log("Cached Group Role: ", cachedGroupRole);
    console.log("Cached User: ", cachedUser);
    console.log("User Role: ", storedUserRole);
  }, []);

  return (
    <AppContext.Provider value={{ menuList, headerList, user, userRole }}>
      {children}
    </AppContext.Provider>
  );
};
