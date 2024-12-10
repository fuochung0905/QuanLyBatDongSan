using Model.BASE;
using Model.HETHONG.MENU.Dtos;
using Model.HETHONG.TAIKHOAN.Dtos;
using Model.HETHONG.TAIKHOAN.Reuquest;

namespace Service.HETHONG.TAIKHOAN
{
    public interface ITAIKHOANService
    {
        BaseResponse<MODELTaiKhoanPhanQuyen> Login(LoginRequest request);
        BaseResponse<MODELTaiKhoan> GetById(GetByIdRequest request);
        BaseResponse<PostTaiKhoanRequest> GetByPost(GetByIdRequest request);
        BaseResponse<GetListPagingResponse> GetListPaging(GetListPagingRequest request);
        BaseResponse<MODELTaiKhoan> GetByUserName(GetByUserNameRequest request);
        BaseResponse<MODELTaiKhoan> Insert(PostTaiKhoanRequest request);
        BaseResponse<MODELTaiKhoan> Update(PostTaiKhoanRequest request);
        BaseResponse<bool> UpdateUserInfo(PostTaiKhoanInfoRequest request);
        BaseResponse<MODELTaiKhoan> ChangePassword(PostChangePasswordRequest request);
        BaseResponse<string> Delete(DeleteRequest request);
        BaseResponse<string> DeleteList(DeleteListRequest request);
        BaseResponse<List<MODELMenu>> GetListMenu(GetListMenuRequest request);
        BaseResponse<List<MODELPhanQuyen>> GetPhanQuyen(GetPhanQuyenRequest request);

        BaseResponse<List<MODELCombobox>> GetAllForCombobox();
        BaseResponse<List<MODELCombobox>> GetComboBoxNguoiQuanLy();

    }
}
