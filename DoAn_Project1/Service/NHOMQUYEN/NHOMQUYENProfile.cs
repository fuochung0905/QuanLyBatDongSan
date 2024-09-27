using AutoMapper;
using Entity.DBContent;
using Model.HETHONG.NHOMQUYEN.Dtos;
using Model.HETHONG.NHOMQUYEN.Requests;

namespace Service.NHOMQUYEN
{
	public class NHOMQUYENProfile : Profile
	{
		public NHOMQUYENProfile()
		{
			CreateMap<MODELNhomQuyen, PHANQUYEN_NHOMQUYEN>();
			CreateMap<PHANQUYEN_NHOMQUYEN, MODELNhomQuyen>();
            CreateMap<PostNhomQuyenRequest, PHANQUYEN_NHOMQUYEN>();
            CreateMap<PHANQUYEN_NHOMQUYEN, PostNhomQuyenRequest>();
        }
	}
}
