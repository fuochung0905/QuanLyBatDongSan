using Model.BASE;
using Model.HETHONG.TAIKHOAN.Dtos;
using Model.HETHONG.TAIKHOAN.Reuquest;

namespace Service.TAIKHOAN
{
	public interface ITAIKHOANService
	{
		BaseResponse<MODELTaiKhoanPhanQuyen> Login(LoginRequest request);



	}
}
