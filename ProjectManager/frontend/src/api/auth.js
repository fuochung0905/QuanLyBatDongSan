import api from "./apiService";

export const login = async (credentials) => {
  try {
    const response = await api.post("/taiKhoan/login", credentials);
    return response.data;
  } catch (error) {
    return { status: false, message: error.response?.data?.message || "Error" };
  }
};
