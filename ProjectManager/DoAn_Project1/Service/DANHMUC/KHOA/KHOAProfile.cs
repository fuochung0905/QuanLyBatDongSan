using AutoMapper;
using Model.DANHMUC.KHOA.Dtos;
using Model.DANHMUC.KHOA.Requests;

namespace Service.DANHMUC.KHOA
{
    public class KHOAProfile : Profile
    {
        public KHOAProfile()
        {
            CreateMap<Entity.DBContent.KHOA, MODELKhoa>();
            CreateMap<MODELKhoa, Entity.DBContent.KHOA>();
            CreateMap<Entity.DBContent.KHOA, PostKhoanRequest>();
            CreateMap<PostKhoanRequest, Entity.DBContent.KHOA>();
        }
    }
}
