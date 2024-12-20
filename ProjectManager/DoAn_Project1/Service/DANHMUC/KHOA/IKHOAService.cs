using Model.BASE;
using Model.DANHMUC.KHOA.Dtos;
using Model.DANHMUC.KHOA.Requests;

namespace Service.DANHMUC.KHOA
{

    public interface IKHOAService
    {
        BaseResponse<GetListPagingResponse> GetList(GetListPagingRequest request);
        BaseResponse<MODELKhoa> GetById(GetByIdRequest request);
        BaseResponse<PostKhoanRequest> GetByPost(GetByIdRequest request);
        BaseResponse<MODELKhoa> Insert(PostKhoanRequest request);
        BaseResponse<MODELKhoa> Update(PostKhoanRequest request);
        BaseResponse<string> Delete(DeleteRequest request);
        BaseResponse<string> DeleteList(DeleteListRequest request);
        BaseResponse<List<MODELCombobox>> GetAllForCombobox();
    }
}
