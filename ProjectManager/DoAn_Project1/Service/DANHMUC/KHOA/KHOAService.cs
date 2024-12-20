using AutoDependencyRegistration.Attributes;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Model.BASE;
using Model.DANHMUC.KHOA.Dtos;
using Model.DANHMUC.KHOA.Requests;
using Repository;

namespace Service.DANHMUC.KHOA
{
    [RegisterClassAsTransient]
    public class KHOAService : IKHOAService
    {
        private IUnitOfWork _unitOfWork;
        private IHttpContextAccessor _contextAccessor;
        private IMapper _mapper;
        public KHOAService(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor, IMapper mapper)
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

                var result = _unitOfWork.GetRepository<MODELKhoa>().ExcuteStoredProcedure("sp_DM_KHOA_GetListPaging", parameters).ToList();
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
        public BaseResponse<MODELKhoa> GetById(GetByIdRequest request)
        {
            var response = new BaseResponse<MODELKhoa>();
            try
            {
                var result = new MODELKhoa();
                var data = _unitOfWork.GetRepository<Entity.DBContent.KHOA>().Find(x => x.Id == request.Id);
                if (data == null)
                    throw new Exception("Không tìm thấy thông tin");
                else
                {
                    result = _mapper.Map<MODELKhoa>(data);
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
        public BaseResponse<PostKhoanRequest> GetByPost(GetByIdRequest request)
        {
            var response = new BaseResponse<PostKhoanRequest>();
            try
            {
                var result = new PostKhoanRequest();
                var data = _unitOfWork.GetRepository<Entity.DBContent.KHOA>().Find(x => x.Id == request.Id);
                if (data == null)
                {
                    result.Id = Guid.NewGuid();
                    result.IsEdit = false;
                }
                else
                {
                    result = _mapper.Map<PostKhoanRequest>(data);
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
        public BaseResponse<MODELKhoa> Insert(PostKhoanRequest request)
        {
            var response = new BaseResponse<MODELKhoa>();
            try
            {
                var isExist = _unitOfWork.GetRepository<Entity.DBContent.KHOA>()
                    .Find(x => x.TenVietTat == request.TenVietTat || x.TenGoi == request.TenGoi);
                if (isExist != null)
                {
                    throw new Exception("Dữ liệu đã tồn tại");
                }
                var add = _mapper.Map<Entity.DBContent.KHOA>(request);
                add.NguoiTao = _contextAccessor.HttpContext.User.Identity.Name;
                add.NgayTao = DateTime.Now;
                add.NguoiSua = _contextAccessor.HttpContext.User.Identity.Name;
                add.NgaySua = DateTime.Now;
                _unitOfWork.GetRepository<Entity.DBContent.KHOA>().Add(add);
                _unitOfWork.Commit();

                response.Data = _mapper.Map<MODELKhoa>(add);
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }

            return response;
        }

        //UPDATE
        public BaseResponse<MODELKhoa> Update(PostKhoanRequest request)
        {
            var response = new BaseResponse<MODELKhoa>();
            try
            {
                var isExist = _unitOfWork.GetRepository<Entity.DBContent.KHOA>()
                    .Find(x => x.Id != request.Id && (x.TenVietTat == request.TenVietTat || x.TenGoi == request.TenGoi));
                if (isExist != null)
                {
                    throw new Exception("Dữ liệu đã tồn tại");
                }
                var update = _unitOfWork.GetRepository<Entity.DBContent.KHOA>().Find(x => x.Id == request.Id);
                if (update != null)
                {
                    _mapper.Map(request, update);
                    update.NguoiSua = _contextAccessor.HttpContext.User.Identity.Name;
                    update.NgaySua = DateTime.Now;

                    _unitOfWork.GetRepository<Entity.DBContent.KHOA>().Update(update);
                    _unitOfWork.Commit();

                    response.Data = _mapper.Map<MODELKhoa>(update);
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
                var delete = _unitOfWork.GetRepository<Entity.DBContent.KHOA>().Find(x => x.Id == request.Id);
                if (delete != null)
                {
                    delete.IsDeleted = true;
                    delete.NguoiXoa = _contextAccessor.HttpContext.User.Identity.Name;
                    delete.NgayXoa = DateTime.Now;

                    _unitOfWork.GetRepository<Entity.DBContent.KHOA>().Update(delete);
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
                    var delete = _unitOfWork.GetRepository<Entity.DBContent.KHOA>().Find(x => x.Id == id);
                    if (delete != null)
                    {
                        delete.IsDeleted = true;
                        delete.NguoiXoa = _contextAccessor.HttpContext.User.Identity.Name;
                        delete.NgayXoa = DateTime.Now;

                        _unitOfWork.GetRepository<Entity.DBContent.KHOA>().Update(delete);
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
            var data = _unitOfWork.GetRepository<Entity.DBContent.KHOA>()
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
