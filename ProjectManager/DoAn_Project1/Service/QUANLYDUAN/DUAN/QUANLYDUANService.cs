using AutoDependencyRegistration.Attributes;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Model.BASE;
using Model.QUANLIDUAN.QLDUAN.Dtos;
using Model.QUANLIDUAN.QLDUAN.Requests;
using Repository;

namespace Service.QUANLYDUAN.DUAN
{
    [RegisterClassAsTransient]
    public class QUANLYDUANService : IQUANLYDUANService
    {
        private IUnitOfWork _unitOfWork;
        private IHttpContextAccessor _contextAccessor;
        private IMapper _mapper;
        public QUANLYDUANService(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

        public BaseResponse<GetListPagingResponse> GetList(PostQuanLyDuAnGetListPaging request)
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
                new SqlParameter("@iKhoaId", request.KhoaId.HasValue ? request.KhoaId : DBNull.Value),
                new SqlParameter("@iLopId", request.LopId.HasValue ? request.LopId : DBNull.Value),
                new SqlParameter("@iPageIndex", request.PageIndex),
                new SqlParameter("@iRowsPerPage", request.RowPerPage),
                iTotalRow
            };

                var result = _unitOfWork.GetRepository<MODELQuanLyDuAn>().ExcuteStoredProcedure("sp_DA_QUANLYDUAN_GetListPaging", parameters).ToList();
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
        public BaseResponse<MODELQuanLyDuAn> GetById(GetByIdRequest request)
        {
            var response = new BaseResponse<MODELQuanLyDuAn>();
            try
            {
                var result = new MODELQuanLyDuAn();
                var data = _unitOfWork.GetRepository<Entity.DBContent.QL_DUAN>().Find(x => x.Id == request.Id);
                if (data == null)
                    throw new Exception("Không tìm thấy thông tin");
                else
                {
                    result = _mapper.Map<MODELQuanLyDuAn>(data);
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
        public BaseResponse<PostQuanLyDuAnRequest> GetByPost(GetByIdRequest request)
        {
            var response = new BaseResponse<PostQuanLyDuAnRequest>();
            try
            {
                var result = new PostQuanLyDuAnRequest();
                var data = _unitOfWork.GetRepository<Entity.DBContent.QL_DUAN>().Find(x => x.Id == request.Id);
                if (data == null)
                {
                    result.Id = Guid.NewGuid();
                    result.IsEdit = false;
                }
                else
                {
                    result = _mapper.Map<PostQuanLyDuAnRequest>(data);
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
        public BaseResponse<MODELQuanLyDuAn> Insert(PostQuanLyDuAnRequest request)
        {
            var response = new BaseResponse<MODELQuanLyDuAn>();
            try
            {
                var isExist = _unitOfWork.GetRepository<Entity.DBContent.QL_DUAN>()
                    .Find(x => x.TenVietTat == request.TenVietTat || x.TenGoi == request.TenGoi);
                if (isExist != null)
                {
                    throw new Exception("Dữ liệu đã tồn tại");
                }
                var add = _mapper.Map<Entity.DBContent.QL_DUAN>(request);
                add.NguoiTao = _contextAccessor.HttpContext.User.Identity.Name;
                add.NgayTao = DateTime.Now;
                add.NguoiSua = _contextAccessor.HttpContext.User.Identity.Name;
                add.NgaySua = DateTime.Now;
                _unitOfWork.GetRepository<Entity.DBContent.QL_DUAN>().Add(add);
                _unitOfWork.Commit();

                response.Data = _mapper.Map<MODELQuanLyDuAn>(add);
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }

            return response;
        }

        //UPDATE
        public BaseResponse<MODELQuanLyDuAn> Update(PostQuanLyDuAnRequest request)
        {
            var response = new BaseResponse<MODELQuanLyDuAn>();
            try
            {
                var isExist = _unitOfWork.GetRepository<Entity.DBContent.QL_DUAN>()
                    .Find(x => x.Id != request.Id && (x.TenVietTat == request.TenVietTat || x.TenGoi == request.TenGoi));
                if (isExist != null)
                {
                    throw new Exception("Dữ liệu đã tồn tại");
                }
                var update = _unitOfWork.GetRepository<Entity.DBContent.QL_DUAN>().Find(x => x.Id == request.Id);
                if (update != null)
                {
                    _mapper.Map(request, update);
                    update.NguoiSua = _contextAccessor.HttpContext.User.Identity.Name;
                    update.NgaySua = DateTime.Now;

                    _unitOfWork.GetRepository<Entity.DBContent.QL_DUAN>().Update(update);
                    _unitOfWork.Commit();

                    response.Data = _mapper.Map<MODELQuanLyDuAn>(update);
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
                var delete = _unitOfWork.GetRepository<Entity.DBContent.QL_DUAN>().Find(x => x.Id == request.Id);
                if (delete != null)
                {
                    delete.IsDeleted = true;
                    delete.NguoiXoa = _contextAccessor.HttpContext.User.Identity.Name;
                    delete.NgayXoa = DateTime.Now;

                    _unitOfWork.GetRepository<Entity.DBContent.QL_DUAN>().Update(delete);
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
                    var delete = _unitOfWork.GetRepository<Entity.DBContent.QL_DUAN>().Find(x => x.Id == id);
                    if (delete != null)
                    {
                        delete.IsDeleted = true;
                        delete.NguoiXoa = _contextAccessor.HttpContext.User.Identity.Name;
                        delete.NgayXoa = DateTime.Now;

                        _unitOfWork.GetRepository<Entity.DBContent.QL_DUAN>().Update(delete);
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
            var data = _unitOfWork.GetRepository<Entity.DBContent.QL_DUAN>()
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
