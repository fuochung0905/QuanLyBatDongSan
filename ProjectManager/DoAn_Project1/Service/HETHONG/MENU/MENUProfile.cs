using AutoMapper;
using Model.HETHONG.MENU.Dtos;
using Model.HETHONG.MENU.Requests;

namespace Service.HETHONG.MENU
{
    public class MENUProfile : Profile
    {
        public MENUProfile()
        {
            CreateMap<Entity.DBContent.SYS_MENU, MODELMenu>();
            CreateMap<MODELMenu, Entity.DBContent.SYS_MENU>();
            CreateMap<Entity.DBContent.SYS_MENU, PostMenuRequest>();
            CreateMap<PostMenuRequest, Entity.DBContent.SYS_MENU>();
        }
    }
}
