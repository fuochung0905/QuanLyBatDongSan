using Model.BASE;
using Model.HETHONG.NHOMQUYEN.Dtos;

namespace Service.HETHONG.NHOMQUYEN
{
    public interface INHOMQUYENService
    {
        BaseResponse<List<MODELNhomQuyen>> GetList();
    }
}
