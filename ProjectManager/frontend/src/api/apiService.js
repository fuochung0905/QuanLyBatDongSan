
import axios from "axios";
const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL || "https://localhost:7078/api", 
  headers: {
    "Content-Type": "application/json", 
  },
});

api.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response) {
      const method = error.config.method.toUpperCase(); 
      const url = error.config.url; 
      if (error.response.status === 401) {
        toast.error(
          `Phiên làm việc đã hết hạn, vui lòng đăng nhập lại. (${method} ${url})`
        );
        localStorage.clear();
        window.location.href = "/"; 
      } else {
        toast.error(
          `Đã xảy ra lỗi với yêu cầu ${method} đến ${url}. Vui lòng thử lại.`
        );
      }
    } else if (error.request) {
      toast.error("Không thể kết nối đến máy chủ. Vui lòng thử lại.");
    } else {
      toast.error("Đã có lỗi xảy ra. Vui lòng thử lại.");
    }

    return Promise.reject(error);
  }
);


export default api;

