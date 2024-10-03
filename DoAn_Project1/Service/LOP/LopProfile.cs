using AutoMapper;
using Model.HETHONG.LOP.Dtos;
using Model.HETHONG.LOP.Requests;
using Model.HETHONG.VAITRO.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.LOP
{
    public class LopProfile : Profile
    {
        public LopProfile() {
            CreateMap<Entity.DBContent.LOPHOC, PostLopRequest>();
            CreateMap<PostLopRequest, Entity.DBContent.LOPHOC>();
            CreateMap<Entity.DBContent.LOPHOC, MODELLop>();
            CreateMap<MODELLop, Entity.DBContent.LOPHOC>();
        }
    }
}
