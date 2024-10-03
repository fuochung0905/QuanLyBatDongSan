using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoDependencyRegistration.Attributes;
using AutoMapper;
using Entity.DBContent;
using Microsoft.Data.SqlClient;
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

        public BaseResponse<List<MODELNhomQuyen>> GetList()
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
        public BaseResponse<GetListPagingResponse> GetList(GetListPagingRequest request)
        {
            var response = new BaseResponse<GetListPagingResponse>();
            try
            {
                SqlParameter iTotalRow = new SqlParameter()
                {
                    ParameterName = "@oTotalRow",
                    SqlDbType = System.Data.SqlDbType.BigInt,
                    Direction = System.Data.ParameterDirection.Output
                };

                var parameters = new[]
                {
                    new SqlParameter("@iTextSearch", request.TextSearch),
                    new SqlParameter("@iPageIndex", request.PageIndex),
                    new SqlParameter("@iRowsPerPage", request.RowPerPage),
                    iTotalRow
                };

                var result = _unitOfWork.GetRepository<MODELNhomQuyen>().ExcuteStoredProcedure("sp_HT_NhomQuyen_GetListPaging", parameters).ToList();
                GetListPagingResponse resposeData = new GetListPagingResponse();
                resposeData.PageIndex = request.PageIndex;
                resposeData.Data = result;
                resposeData.TotalRow = Convert.ToInt32(iTotalRow.Value);

                response.Data = resposeData;

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
                var update = _unitOfWork.GetRepository<PHANQUYEN_NHOMQUYEN>().Find(x => x.Id == request.Id);
                if (update == null)
                {
                    throw new Exception("Dữ liệu đã tồn tại");
                }
                else
                {
                    var check = _unitOfWork.GetRepository<PHANQUYEN_NHOMQUYEN>().GetAll(x => x.Id != request.Id && request.TenGoi == x.TenGoi);
                    if (check == null)
                    {
                        throw new Exception("Tên không được trùng");
                    }


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
            //BaseResponse<List<MODELCombobox>> response = new BaseResponse<List<MODELCombobox>>();
            //var data = _unitOfWork.GetRepository<PHANQUYEN_NHOMQUYEN>().GetAll(x=>x.IsActived).ToList();
            //response.Data = data.Select(x => new MODELCombobox()
            //{
            //    Value = x.Id.ToString(),
            //    Text = x.TenGoi

            //}).OrderBy(x=>x.Text).ToList();
            //return response;
            return null;
        }

        public BaseResponse<string> Delete(DeleteRequest request)
        {
            var response = new BaseResponse<string>();
            try
            {
                var delete = _unitOfWork.GetRepository<PHANQUYEN_NHOMQUYEN>().Find(x => x.Id == request.Id);
                if (delete != null)
                {
                }
                else
                {
                    throw new Exception("Không tìm thấy dữ liệu");
                }
                _unitOfWork.Commit();
                response.Data = request.Id.ToString();
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }

            return response;
        }

        public BaseResponse<string> DeleteList(DeleteListRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
