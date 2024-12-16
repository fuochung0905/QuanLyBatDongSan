import './App.css';
import { BrowserRouter as Router, Routes, Route, Navigate } from "react-router-dom";
import Login from "./page/Login";
import Dashboard from "./page/Dashboard";
import Profile from "./page/Content/Profile";

import 'react-toastify/dist/ReactToastify.css'; 

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Login />} />
        <Route path="/dashboard" element={<Dashboard />}>
         
          <Route index element={<Navigate to="profile" />} />
          <Route path="profile" element={<Profile />} />
          
        </Route>
      </Routes>
   
    </Router>
    
  );
}

export default App;
