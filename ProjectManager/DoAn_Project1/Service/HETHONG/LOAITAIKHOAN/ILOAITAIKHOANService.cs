using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODELS;
using MODELS.BASE;
using MODELS.DANHMUC.GIAIDOANDUAN.Dtos;
using MODELS.DANHMUC.GIAIDOANDUAN.Requests;
using MODELS.DANHMUC.LOAITAIKHOAN.Dtos;
using MODELS.DANHMUC.LOAITAIKHOAN.Requests;

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
        BaseResponse<List<MODELCombobox>> GetAllForCombobox(GetAllRequest request);
    }
}
