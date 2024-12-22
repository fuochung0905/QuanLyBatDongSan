using Model.BASE;
using Model.QUANLIDUAN.QLDUAN.Dtos;
using Model.QUANLIDUAN.QLDUAN.Requests;

namespace Service.QUANLYDUAN.DUAN
{
    public interface IQUANLYDUANService
    {
        BaseResponse<GetListPagingResponse> GetList(PostQuanLyDuAnGetListPaging request);
        BaseResponse<MODELQuanLyDuAn> GetById(GetByIdRequest request);
        BaseResponse<PostQuanLyDuAnRequest> GetByPost(GetByIdRequest request);
        BaseResponse<MODELQuanLyDuAn> Insert(PostQuanLyDuAnRequest request);
        BaseResponse<MODELQuanLyDuAn> Update(PostQuanLyDuAnRequest request);
        BaseResponse<string> Delete(DeleteRequest request);
        BaseResponse<string> DeleteList(DeleteListRequest request);
        BaseResponse<List<MODELCombobox>> GetAllForCombobox();
    }
}
