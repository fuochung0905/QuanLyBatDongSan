using AutoMapper;
using Model.HETHONG.LOAITAIKHOAN.Dtos;
using Model.HETHONG.LOAITAIKHOAN.Requests;

namespace REPONSITORY.DANHMUC.LOAITAIKHOAN
{
    public class LOAITAIKHOANProfile : Profile
    {
        public LOAITAIKHOANProfile()
        {
            CreateMap<Entity.DBContent.LOAITAIKHOAN, MODELLoaiTaiKhoan>();
            CreateMap<MODELLoaiTaiKhoan, Entity.DBContent.LOAITAIKHOAN>();
            CreateMap<Entity.DBContent.LOAITAIKHOAN, PostLoaiTaiKhoanRequest>();
            CreateMap<PostLoaiTaiKhoanRequest, Entity.DBContent.LOAITAIKHOAN>();
        }
    }
}
