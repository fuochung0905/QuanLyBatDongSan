using Model.BASE;
using Model.DANHMUC.GIAIDOAN.Dtos;
using Model.DANHMUC.GIAIDOAN.Requests;

namespace Service.DANHMUC.GIAIDOAN
{
    public interface IGIAIDOANService
    {
        BaseResponse<GetListPagingResponse> GetList(GetListPagingRequest request);
        BaseResponse<MODELGiaiDoan> GetById(GetByIdRequest request);
        BaseResponse<PostGiaiDoanRequest> GetByPost(GetByIdRequest request);
        BaseResponse<MODELGiaiDoan> Insert(PostGiaiDoanRequest request);
        BaseResponse<MODELGiaiDoan> Update(PostGiaiDoanRequest request);
        BaseResponse<string> Delete(DeleteRequest request);
        BaseResponse<string> DeleteList(DeleteListRequest request);
        BaseResponse<List<MODELCombobox>> GetAllForCombobox();
    }
}
