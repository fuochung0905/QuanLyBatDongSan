using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.BASE;
using Model.HETHONG.VAITRO.Dtos;
using Model.HETHONG.VAITRO.Requests;

namespace Service.VAITRO
{
    public interface IVAITROService
    {
        BaseResponse<GetListPagingResponse> GetListPaging(GetListPagingRequest request);
        BaseResponse<PostVaiTroRequest> GetByPost(GetByIdRequest request);
        BaseResponse<MODELVaiTro> Insert(PostVaiTroRequest request);
        BaseResponse<MODELVaiTro> Update(PostVaiTroRequest request);
        BaseResponse<List<MODELCombobox>> GetAllForCombobox();
        BaseResponse<string> Delete(DeleteRequest request);
        BaseResponse<string> DeleteList(DeleteListRequest request);



    }
}
