using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Model.HETHONG.VAITRO.Dtos;
using Model.HETHONG.VAITRO.Requests;

namespace Service.VAITRO
{
    public class VAITROProfile : Profile
    {
        public VAITROProfile()
        {
            CreateMap<Entity.DBContent.VAITRO, PostVaiTroRequest>();
            CreateMap<PostVaiTroRequest, Entity.DBContent.VAITRO>();
            CreateMap<MODELVaiTro, Entity.DBContent.VAITRO>();
            CreateMap<Entity.DBContent.VAITRO, MODELVaiTro>();
        }
    }
}
