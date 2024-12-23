using Model.BASE;
using Model.HETHONG.MENU.Dtos;
using Model.HETHONG.MENU.Requests;

namespace Service.HETHONG.MENU
{
    public interface IMENUService
    {
        BaseResponse<GetListPagingResponse> GetListPaging(GetListPagingRequest request);
        BaseResponse<List<MODELMenu>> GetAll( );
        BaseResponse<MODELMenu> GetById(GetMenuByIdRequest request);
        BaseResponse<PostMenuRequest> GetByPost(GetMenuByIdRequest request);
        BaseResponse<MODELMenu> Insert(PostMenuRequest request);
        BaseResponse<MODELMenu> Update(PostMenuRequest request);
    }
}
