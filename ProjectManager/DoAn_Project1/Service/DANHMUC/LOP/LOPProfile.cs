using AutoMapper;
using Model.DANHMUC.LOP.Dtos;
using Model.DANHMUC.LOP.Requests;

namespace Service.DANHMUC.LOP
{
    public class LOPProfile : Profile
    {
        public LOPProfile()
        {
            CreateMap<Entity.DBContent.LOP, MODELLop>();
            CreateMap<MODELLop, Entity.DBContent.LOP>();
            CreateMap<Entity.DBContent.LOP, PostLopRequest>();
            CreateMap<PostLopRequest, Entity.DBContent.LOP>();
        }
    }
}
