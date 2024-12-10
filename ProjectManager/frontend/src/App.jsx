
import './App.css'
import Navbar from './components/layout/Navbar'
import Slidebar from './components/layout/Slidebar'
import LayoutBody from './components/layout/LayoutBody'
import HeaderLayout from './components/layout/HeaderLayout'

function App() {
  return (
    <div >
    <div class="sidebar">
      <nav class="menu">
        <ul>
          <li><a href="#">Trang chủ</a></li>
          <li><a href="#">Quản lý sản phẩm</a></li>
          <li class="active"><a href="#">Quản lý QR code</a></li>
          <li><a href="#">Quản lý khách hàng</a></li>
          <li><a href="#">Chương trình thành viên</a></li>
          <li><a href="#">Gửi thông báo</a></li>
        </ul>
      </nav>
    </div>
    <div class="main-layout">
      <header class="main-header">
        <h1>Quản lý QR code / Tạo QR code</h1>
      </header>
    
      <section class="main-body">
        <div className='body-content'>
          <h2>Content</h2>
        </div>
      </section>
    </div>
  </div>
  )
}

export default App
