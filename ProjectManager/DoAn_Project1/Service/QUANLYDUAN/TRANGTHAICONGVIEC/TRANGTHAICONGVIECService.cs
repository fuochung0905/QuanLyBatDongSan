using AutoDependencyRegistration.Attributes;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Model.BASE;
using Model.QUANLIDUAN.TRANGTHAICONGVIEC.Dtos;
using Model.QUANLIDUAN.TRANGTHAICONGVIEC.Requests;
using Repository;

namespace Service.QUANLYDUAN.TRANGTHAICONGVIEC
{
    [RegisterClassAsTransient]
    public class TRANGTHAICONGVIECService : ITRANGTHAICONGVIECService
    {
        private IUnitOfWork _unitOfWork;
        private IHttpContextAccessor _contextAccessor;
        private IMapper _mapper;
        public TRANGTHAICONGVIECService(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

        public BaseResponse<GetListPagingResponse> GetList(GetListPagingRequest request)
        {
            BaseResponse<GetListPagingResponse> response = new BaseResponse<GetListPagingResponse>();
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

                var result = _unitOfWork.GetRepository<MODELTrangThaiCongViec>().ExcuteStoredProcedure("sp_DM_TRANGTHAICONGVIEC_GetListPaging", parameters).ToList();
                GetListPagingResponse responseData = new GetListPagingResponse();
                responseData.Data = result;
                responseData.TotalRow = Convert.ToInt32(iTotalRow.Value);
                responseData.PageIndex = request.PageIndex;

                response.Data = responseData;
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }
            return response;
        }
        public BaseResponse<MODELTrangThaiCongViec> GetById(GetByIdRequest request)
        {
            var response = new BaseResponse<MODELTrangThaiCongViec>();
            try
            {
                var result = new MODELTrangThaiCongViec();
                var data = _unitOfWork.GetRepository<Entity.DBContent.TRANGTHAICONGVIEC>().Find(x => x.Id == request.Id);
                if (data == null)
                    throw new Exception("Không tìm thấy thông tin");
                else
                {
                    result = _mapper.Map<MODELTrangThaiCongViec>(data);
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

        //GET BY POST (INSERT/UPDATE)
        public BaseResponse<PostTrangThaiCongViecRequest> GetByPost(GetByIdRequest request)
        {
            var response = new BaseResponse<PostTrangThaiCongViecRequest>();
            try
            {
                var result = new PostTrangThaiCongViecRequest();
                var data = _unitOfWork.GetRepository<Entity.DBContent.TRANGTHAICONGVIEC>().Find(x => x.Id == request.Id);
                if (data == null)
                {
                    result.Id = Guid.NewGuid();
                    result.IsEdit = false;
                }
                else
                {
                    result = _mapper.Map<PostTrangThaiCongViecRequest>(data);
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

        //INSERT
        public BaseResponse<MODELTrangThaiCongViec> Insert(PostTrangThaiCongViecRequest request)
        {
            var response = new BaseResponse<MODELTrangThaiCongViec>();
            try
            {
                var isExist = _unitOfWork.GetRepository<Entity.DBContent.TRANGTHAICONGVIEC>()
                    .Find(x => x.TenGoi == request.TenGoi);
                if (isExist != null)
                {
                    throw new Exception("Dữ liệu đã tồn tại");
                }
                var add = _mapper.Map<Entity.DBContent.TRANGTHAICONGVIEC>(request);
                add.NguoiTao = _contextAccessor.HttpContext.User.Identity.Name;
                add.NgayTao = DateTime.Now;
                add.NguoiSua = _contextAccessor.HttpContext.User.Identity.Name;
                add.NgaySua = DateTime.Now;
                _unitOfWork.GetRepository<Entity.DBContent.TRANGTHAICONGVIEC>().Add(add);
                _unitOfWork.Commit();

                response.Data = _mapper.Map<MODELTrangThaiCongViec>(add);
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }

            return response;
        }

        //UPDATE
        public BaseResponse<MODELTrangThaiCongViec> Update(PostTrangThaiCongViecRequest request)
        {
            var response = new BaseResponse<MODELTrangThaiCongViec>();
            try
            {
                var isExist = _unitOfWork.GetRepository<Entity.DBContent.TRANGTHAICONGVIEC>()
                    .Find(x => x.Id != request.Id && x.TenGoi == request.TenGoi);
                if (isExist != null)
                {
                    throw new Exception("Dữ liệu đã tồn tại");
                }
                var update = _unitOfWork.GetRepository<Entity.DBContent.TRANGTHAICONGVIEC>().Find(x => x.Id == request.Id);
                if (update != null)
                {
                    _mapper.Map(request, update);
                    update.NguoiSua = _contextAccessor.HttpContext.User.Identity.Name;
                    update.NgaySua = DateTime.Now;

                    _unitOfWork.GetRepository<Entity.DBContent.TRANGTHAICONGVIEC>().Update(update);
                    _unitOfWork.Commit();

                    response.Data = _mapper.Map<MODELTrangThaiCongViec>(update);
                }
                else
                {
                    throw new Exception("Không tìm thấy dữ liệu");
                }
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }

            return response;
        }

        //DELETE
        public BaseResponse<string> Delete(DeleteRequest request)
        {
            var response = new BaseResponse<string>();
            try
            {
                var delete = _unitOfWork.GetRepository<Entity.DBContent.TRANGTHAICONGVIEC>().Find(x => x.Id == request.Id);
                if (delete != null)
                {
                    delete.IsDeleted = true;
                    delete.NguoiXoa = _contextAccessor.HttpContext.User.Identity.Name;
                    delete.NgayXoa = DateTime.Now;

                    _unitOfWork.GetRepository<Entity.DBContent.TRANGTHAICONGVIEC>().Update(delete);
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

        //DELETE LIST
        public BaseResponse<string> DeleteList(DeleteListRequest request)
        {
            var response = new BaseResponse<string>();
            try
            {
                foreach (var id in request.Ids)
                {
                    var delete = _unitOfWork.GetRepository<Entity.DBContent.TRANGTHAICONGVIEC>().Find(x => x.Id == id);
                    if (delete != null)
                    {
                        delete.IsDeleted = true;
                        delete.NguoiXoa = _contextAccessor.HttpContext.User.Identity.Name;
                        delete.NgayXoa = DateTime.Now;

                        _unitOfWork.GetRepository<Entity.DBContent.TRANGTHAICONGVIEC>().Update(delete);
                    }
                    else
                    {
                        throw new Exception("Không tìm thấy dữ liệu");
                    }
                }
                _unitOfWork.Commit();
                response.Data = string.Join(',', request);
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }

            return response;
        }

        //GET ALL FOR COMBOBOX
        public BaseResponse<List<MODELCombobox>> GetAllForCombobox()
        {
            BaseResponse<List<MODELCombobox>> response = new();
            var data = _unitOfWork.GetRepository<Entity.DBContent.TRANGTHAICONGVIEC>()
                .GetAll(x => x.IsActived && !x.IsDeleted).ToList();
            response.Data = data.Select(x => new MODELCombobox
            {
                Text = x.TenGoi,
                Value = x.Id.ToString(),
            }).OrderBy(x => x.Text).ToList();

            return response;
        }
    }
}
