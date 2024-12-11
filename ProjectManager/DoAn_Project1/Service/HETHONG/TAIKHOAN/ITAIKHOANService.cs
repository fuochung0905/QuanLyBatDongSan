using Model.BASE;
using Model.HETHONG.MENU.Dtos;
using Model.HETHONG.TAIKHOAN.Dtos;
using Model.HETHONG.TAIKHOAN.Reuquest;

namespace Service.HETHONG.TAIKHOAN
{
    public interface ITAIKHOANService
    {
        BaseResponse<MODELTaiKhoanPhanQuyen> Login(LoginRequest request);
        BaseResponse<List<MODELMenu>> GetListMenu(GetListMenuRequest request);
        BaseResponse<List<MODELPhanQuyen>> GetPhanQuyen(GetPhanQuyenRequest request);
        BaseResponse<List<MODELCombobox>> GetAllForCombobox();

    }
}
