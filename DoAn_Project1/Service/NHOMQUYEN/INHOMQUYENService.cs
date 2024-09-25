using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.BASE;
using Model.HETHONG.NHOMQUYEN.Dtos;
using Model.HETHONG.NHOMQUYEN.Requests;

namespace Service.NHOMQUYEN
{
    public interface INHOMQUYENService
    {
        BaseResponse<List<MODELNhomQuyen>> GetList(GetListPagingRequest request);
        BaseResponse<List<MODELNhomQuyen>> GetList();
        BaseResponse<PostNhomQuyenRequest> GetByPost(GetByIdRequest request);
        BaseResponse<MODELNhomQuyen> Insert(PostNhomQuyenRequest request);
        BaseResponse<MODELNhomQuyen> Update(PostNhomQuyenRequest request);
        BaseResponse<List<MODELCombobox>> GetAllCombobox();
    }
}
