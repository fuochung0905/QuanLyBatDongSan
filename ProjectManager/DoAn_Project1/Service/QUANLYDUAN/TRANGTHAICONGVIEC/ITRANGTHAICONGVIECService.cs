using Model.BASE;
using Model.QUANLIDUAN.TRANGTHAICONGVIEC.Dtos;
using Model.QUANLIDUAN.TRANGTHAICONGVIEC.Requests;

namespace Service.QUANLYDUAN.TRANGTHAICONGVIEC
{
    public interface ITRANGTHAICONGVIECService
    {
        BaseResponse<GetListPagingResponse> GetList(GetListPagingRequest request);
        BaseResponse<MODELTrangThaiCongViec> GetById(GetByIdRequest request);
        BaseResponse<PostTrangThaiCongViecRequest> GetByPost(GetByIdRequest request);
        BaseResponse<MODELTrangThaiCongViec> Insert(PostTrangThaiCongViecRequest request);
        BaseResponse<MODELTrangThaiCongViec> Update(PostTrangThaiCongViecRequest request);
        BaseResponse<string> Delete(DeleteRequest request);
        BaseResponse<string> DeleteList(DeleteListRequest request);
        BaseResponse<List<MODELCombobox>> GetAllForCombobox();
    }
}
