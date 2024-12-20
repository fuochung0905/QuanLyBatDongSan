using Model.BASE;
using Model.DANHMUC.KHOA.Dtos;
using Model.DANHMUC.LOP.Dtos;
using Model.DANHMUC.LOP.Requests;

namespace Service.DANHMUC.LOP
{
    public interface ILOPService
    {
        BaseResponse<GetListPagingResponse> GetList(GetListPagingRequest request);
        BaseResponse<MODELLop> GetById(GetByIdRequest request);
        BaseResponse<PostLopRequest> GetByPost(GetByIdRequest request);
        BaseResponse<MODELLop> Insert(PostLopRequest request);
        BaseResponse<MODELLop> Update(PostLopRequest request);
        BaseResponse<string> Delete(DeleteRequest request);
        BaseResponse<string> DeleteList(DeleteListRequest request);
        BaseResponse<List<MODELCombobox>> GetAllForCombobox();
    }
}
