using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ENTITIES.DBContent;
using MODELS.DANHMUC.LOAITAIKHOAN.Dtos;
using MODELS.DANHMUC.LOAITAIKHOAN.Requests;

namespace REPONSITORY.DANHMUC.LOAITAIKHOAN
{
    public class LOAITAIKHOANProfile : Profile
    {
        public LOAITAIKHOANProfile()
        {
            CreateMap<DM_LOAITAIKHOAN, MODELLoaiTaiKhoan>();
            CreateMap<MODELLoaiTaiKhoan, DM_LOAITAIKHOAN>();
            CreateMap<DM_LOAITAIKHOAN, PostLoaiTaiKhoanRequest>();
            CreateMap<PostLoaiTaiKhoanRequest, DM_LOAITAIKHOAN>();
        }
    }
}
