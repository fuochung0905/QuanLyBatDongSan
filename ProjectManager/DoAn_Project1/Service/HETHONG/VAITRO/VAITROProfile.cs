using AutoMapper;
using Entity.DBContent;
using Model.HETHONG.NHOMQUYEN.Dtos;
using Model.HETHONG.VAITRO.Dtos;
using Model.HETHONG.VAITRO.Requests;

namespace REPONSITORY.HETHONG.MENU
{
    public class VAITROProfile : Profile
    {
        public VAITROProfile()
        {
            CreateMap<Entity.DBContent.VAITRO, MODELVaiTro>();
            CreateMap<MODELVaiTro, Entity.DBContent.VAITRO>();
            CreateMap<PostVaiTroRequest, Entity.DBContent.VAITRO>();
            CreateMap<Entity.DBContent.VAITRO, PostVaiTroRequest>();
            CreateMap<PHANQUYEN, MODELVaiTro_PhanQuyen>();
            CreateMap<PHANQUYEN_NHOMQUYEN, MODELNhomQuyen>();
            CreateMap<PostPhanQuyenVaiTroRequest, PHANQUYEN>();
        }
    }
}
