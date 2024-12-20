using AutoMapper;
using Model.QUANLIDUAN.TRANGTHAICONGVIEC.Dtos;
using Model.QUANLIDUAN.TRANGTHAICONGVIEC.Requests;

namespace Service.QUANLYDUAN.TRANGTHAICONGVIEC
{
    public class TRANGTHAICONGVIECProfile : Profile
    {
        public TRANGTHAICONGVIECProfile() {
            CreateMap<Entity.DBContent.TRANGTHAICONGVIEC, MODELTrangThaiCongViec>();
            CreateMap<MODELTrangThaiCongViec, Entity.DBContent.TRANGTHAICONGVIEC>();
            CreateMap<PostTrangThaiCongViecRequest, Entity.DBContent.TRANGTHAICONGVIEC>();
            CreateMap<Entity.DBContent.TRANGTHAICONGVIEC, PostTrangThaiCongViecRequest>();
        }
    }
}
