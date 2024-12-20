using AutoDependencyRegistration.Attributes;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Model.BASE;
using Model.DANHMUC.LOP.Dtos;
using Model.DANHMUC.LOP.Requests;
using Repository;

namespace Service.DANHMUC.LOP
{
    [RegisterClassAsTransient]
    public class LOPService : ILOPService
    {
        private IUnitOfWork _unitOfWork;
        private IHttpContextAccessor _contextAccessor;
        private IMapper _mapper;
        public LOPService(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor, IMapper mapper)
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

                var result = _unitOfWork.GetRepository<MODELLop>().ExcuteStoredProcedure("sp_DM_KHOA_GetListPaging", parameters).ToList();
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
        public BaseResponse<MODELLop> GetById(GetByIdRequest request)
        {
            var response = new BaseResponse<MODELLop>();
            try
            {
                var result = new MODELLop();
                var data = _unitOfWork.GetRepository<Entity.DBContent.KHOA>().Find(x => x.Id == request.Id);
                if (data == null)
                    throw new Exception("Không tìm thấy thông tin");
                else
                {
                    result = _mapper.Map<MODELLop>(data);
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
        public BaseResponse<PostLopRequest> GetByPost(GetByIdRequest request)
        {
            var response = new BaseResponse<PostLopRequest>();
            try
            {
                var result = new PostLopRequest();
                var data = _unitOfWork.GetRepository<Entity.DBContent.KHOA>().Find(x => x.Id == request.Id);
                if (data == null)
                {
                    result.Id = Guid.NewGuid();
                    result.IsEdit = false;
                }
                else
                {
                    result = _mapper.Map<PostLopRequest>(data);
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
        public BaseResponse<MODELLop> Insert(PostLopRequest request)
        {
            var response = new BaseResponse<MODELLop>();
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

                response.Data = _mapper.Map<MODELLop>(add);
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }

            return response;
        }

        //UPDATE
        public BaseResponse<MODELLop> Update(PostLopRequest request)
        {
            var response = new BaseResponse<MODELLop>();
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

                    response.Data = _mapper.Map<MODELLop>(update);
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
