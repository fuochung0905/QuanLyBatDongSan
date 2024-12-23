using AutoMapper;
using Model.DANHMUC.GIAIDOAN.Dtos;
using Model.DANHMUC.GIAIDOAN.Requests;

namespace Service.DANHMUC.GIAIDOAN
{
    public class GIAIDOANProfile : Profile
    {
        public GIAIDOANProfile()
        {
            CreateMap<Entity.DBContent.GIAIDOAN, MODELGiaiDoan>();
            CreateMap<MODELGiaiDoan, Entity.DBContent.GIAIDOAN>();
            CreateMap<Entity.DBContent.GIAIDOAN, PostGiaiDoanRequest>();
            CreateMap<PostGiaiDoanRequest, Entity.DBContent.GIAIDOAN>();
        }
    }
}
