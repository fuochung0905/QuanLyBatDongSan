import './App.css';
import { BrowserRouter as Router, Routes, Route, Navigate } from "react-router-dom";
import Login from "./page/Login";
import Dashboard from "./page/Dashboard";
import Profile from "./page/Content/Profile";
import MenuPage from './page/Content/quanTri/MenuPage';
import 'react-toastify/dist/ReactToastify.css'; 
import NhomQuyenPage from './page/Content/quanTri/NhomQuyenPage';
import DA_QLQA from './page/Content/duAn/DA_QLDA';
import VaiTroPage from './page/Content/heThong/vaiTroPage';
import LoaiTaiKhoanPage from './page/Content/heThong/LoaiTaiKhoanPage';
import TaiKhoanPage from './page/Content/heThong/TaiKhoanPage';
import { AppProvider } from './data/AppContext.jsx'; 
import DA_Workflow from './page/Content/duAn/DA_Workflow.jsx';
import KhoaPage from './page/Content/danhMuc/KhoaPage.jsx';
import LopPage from './page/Content/danhMuc/LopPage.jsx';
import GiaiDoanDuAnPage from './page/Content/danhMuc/GiaiDoanDuAnPage.jsx';

function App() {
  const handleLogout = () => {
    localStorage.clear();
  };
  return (
    
      <Router>
          <Routes>
            <Route path="/" element={<Login />} />
            <Route path="/dashboard" element={<Dashboard />}>
            <Route index element={<Navigate to="taskBoard" />} />
            <Route path="taskBoard" element={<Profile />} />
            {/* ADMIN */}
            <Route path="menu" element={<MenuPage />}/>
            <Route path="nhomQuyen" element={<NhomQuyenPage/>}/>
            {/* HETHONG */}
            <Route path="vaitro" element={<VaiTroPage/>}/>
            <Route path="loaiTaiKhoan" element={<LoaiTaiKhoanPage/>}/>
            <Route path="taiKhoan" element={<TaiKhoanPage/>}/>
            {/* DUAN */}
            <Route path="quanlyduan" element={<DA_QLQA/>}/>
            <Route path="quanlycongviec" element={<DA_QLQA/>}/>
            <Route path="donVi" element={<DA_Workflow/>}/>
            <Route path="trangthaicongviec" element={<Profile/>}/>

            {/* DANH MỤC */}
            <Route path="khoa" element={<KhoaPage/>}/>
            <Route path="lop" element={<LopPage/>}/>
            <Route path="giaidoanduan" element={<GiaiDoanDuAnPage/>}/>
          </Route>
            <Route path="*" element={
              <>
                {handleLogout()} 
                <Navigate to="" /> 
              </>
            } />
          </Routes>
        </Router>
  );
}

export default App;
