using AutoMapper;
using Model.QUANLIDUAN.QLDUAN.Dtos;
using Model.QUANLIDUAN.QLDUAN.Requests;

namespace Service.QUANLYDUAN.DUAN
{
    public class QUANLYDUANProfile : Profile
    {
        public QUANLYDUANProfile()
        {
            CreateMap<Entity.DBContent.QL_DUAN, MODELQuanLyDuAn>();
            CreateMap<MODELQuanLyDuAn, Entity.DBContent.QL_DUAN>();
            CreateMap<Entity.DBContent.QL_DUAN, PostQuanLyDuAnRequest>();
            CreateMap<PostQuanLyDuAnRequest, Entity.DBContent.QL_DUAN>();
        }
    }
}
