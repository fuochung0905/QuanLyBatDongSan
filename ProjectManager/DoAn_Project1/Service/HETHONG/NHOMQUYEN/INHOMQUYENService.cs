using Model.BASE;
using Model.HETHONG.NHOMQUYEN.Dtos;
using Model.HETHONG.NHOMQUYEN.Requests;

namespace Service.HETHONG.NHOMQUYEN
{
    public interface INHOMQUYENService
    {
        BaseResponse<List<MODELNhomQuyen>> GetList();
        BaseResponse<MODELNhomQuyen> GetById(GetByIdRequest request);
        BaseResponse<PostNhomQuyenRequest> GetByPost(GetByIdRequest request);
        BaseResponse<MODELNhomQuyen> Insert(PostNhomQuyenRequest request);
        BaseResponse<MODELNhomQuyen> Update(PostNhomQuyenRequest request);
        BaseResponse<List<MODELNhomQuyen>> GetAll();
        BaseResponse<List<MODELCombobox>> GetAllForCombobox();
        BaseResponse<List<MODELCombobox>> GetAllParentForCombobox();
    }
}
