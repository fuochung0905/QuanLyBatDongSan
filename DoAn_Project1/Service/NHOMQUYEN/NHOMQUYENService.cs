using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoDependencyRegistration.Attributes;
using AutoMapper;
using Entity.DBContent;
using Model.BASE;
using Model.HETHONG.NHOMQUYEN.Dtos;
using Model.HETHONG.NHOMQUYEN.Requests;
using Repository;

namespace Service.NHOMQUYEN
{
    [RegisterClassAsTransient]
    public class NHOMQUYENService : INHOMQUYENService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public NHOMQUYENService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public BaseResponse<List<MODELNhomQuyen>> GetList(GetListPagingRequest request)
        {
            var response = new BaseResponse<List<MODELNhomQuyen>>();
            try
            {
                var data = _unitOfWork.GetRepository<Entity.DBContent.PHANQUYEN_NHOMQUYEN>().GetAll()
                    .OrderBy(x => x.Sort);
                response.Data = _mapper.Map<List<MODELNhomQuyen>>(data);

            }
            catch (Exception e)
            {
                response.Error = true;
                response.Message = e.Message;
            }

            return response;
        }
        public BaseResponse<PostNhomQuyenRequest> GetByPost(GetByIdRequest request)
        {
            var response = new BaseResponse<PostNhomQuyenRequest>();
            try
            {
                var result = new PostNhomQuyenRequest();
                var data = _unitOfWork.GetRepository<PHANQUYEN_NHOMQUYEN>().Find(x => x.Id == request.Id);
                if (data == null)
                {
                    result.Id = Guid.NewGuid();
                    result.IsEdit = false;
                }
                else
                {
                    result = _mapper.Map<PostNhomQuyenRequest>(data);
                    result.IsEdit = true;
                }
                response.Data = result;
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }

            return response;
        }

        public BaseResponse<MODELNhomQuyen> Insert(PostNhomQuyenRequest request)
        {
            var response = new BaseResponse<MODELNhomQuyen>();
            try
            {
                var add = _mapper.Map<PHANQUYEN_NHOMQUYEN>(request);
                _unitOfWork.GetRepository<PHANQUYEN_NHOMQUYEN>().add(add);
                _unitOfWork.Commit();

                response.Data = _mapper.Map<MODELNhomQuyen>(add);
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;  
            }

            return response;
        }

        public BaseResponse<MODELNhomQuyen> Update(PostNhomQuyenRequest request)
        {
            var response = new BaseResponse<MODELNhomQuyen>();
            try
            {
                var update = _unitOfWork.GetRepository<PHANQUYEN_NHOMQUYEN>().Find(x => x.TenGoi == request.TenGoi);
                if (update != null)
                {
                    throw new Exception("Dữ liệu đã tồn tại");
                }
                else
                {
                    _mapper.Map(request, update);
                    _unitOfWork.GetRepository<PHANQUYEN_NHOMQUYEN>().update(update);
                    _unitOfWork.Commit();

                    response.Data = _mapper.Map<MODELNhomQuyen>(update);
                }
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }

            return response;
        }

        public BaseResponse<List<MODELCombobox>> GetAllCombobox()
        {
            BaseResponse<List<MODELCombobox>> response = new BaseResponse<List<MODELCombobox>>();
            var data = _unitOfWork.GetRepository<PHANQUYEN_NHOMQUYEN>().GetAll(x=>x.IsActived).ToList();
            response.Data = data.Select(x => new MODELCombobox()
            {
                Value = x.Id.ToString(),
                Text = x.TenGoi
            
            }).OrderBy(x=>x.Text).ToList();
            return response;
        }
    }
}
