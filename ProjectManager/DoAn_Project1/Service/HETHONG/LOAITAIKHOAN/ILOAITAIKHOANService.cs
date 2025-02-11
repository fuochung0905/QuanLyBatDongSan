﻿using Model.BASE;
using Model.HETHONG.LOAITAIKHOAN.Dtos;
using Model.HETHONG.LOAITAIKHOAN.Requests;

namespace REPONSITORY.DANHMUC.LOAITAIKHOAN
{
    public interface ILOAITAIKHOANService
    {
        BaseResponse<GetListPagingResponse> GetList(GetListPagingRequest request);
        BaseResponse<MODELLoaiTaiKhoan> GetById(GetByIdRequest request);
        BaseResponse<PostLoaiTaiKhoanRequest> GetByPost(GetByIdRequest request);
        BaseResponse<MODELLoaiTaiKhoan> Insert(PostLoaiTaiKhoanRequest request);
        BaseResponse<MODELLoaiTaiKhoan> Update(PostLoaiTaiKhoanRequest request);
        BaseResponse<string> Delete(DeleteRequest request);
        BaseResponse<string> DeleteList(DeleteListRequest request);
        BaseResponse<List<MODELCombobox>> GetAllForCombobox();
    }
}
