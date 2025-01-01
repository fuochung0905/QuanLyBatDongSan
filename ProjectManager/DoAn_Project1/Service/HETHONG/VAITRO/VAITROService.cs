using AutoDependencyRegistration.Attributes;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Model.BASE;
using Model.HETHONG.VAITRO.Dtos;
using Model.HETHONG.VAITRO.Requests;
using Repository;


namespace REPONSITORY.HETHONG.VAITRO
{
    [RegisterClassAsTransient]
    public class VAITROService : IVAITROService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IHttpContextAccessor _contextAccessor;

        public VAITROService(
            IHttpContextAccessor contextAccessor,
            IUnitOfWork unitOfWork,
            IMapper mapper
        )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

        //GET LIST PAGING
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

                var result = _unitOfWork.GetRepository<MODELVaiTro>().ExcuteStoredProcedure("sp_VaiTro_GetListPaging", parameters).ToList();
                GetListPagingResponse resposeData = new GetListPagingResponse();
                resposeData.PageIndex = request.PageIndex;
                resposeData.Data = result;
                resposeData.TotalRow = Convert.ToInt32(iTotalRow.Value);
                response.Data = resposeData;
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }

            return response;
        }

        //GET BY ID
        public BaseResponse<MODELVaiTro> GetById(GetByIdRequest request)
        {
            var response = new BaseResponse<MODELVaiTro>();
            try
            {
                var result = new MODELVaiTro();
                var data = _unitOfWork.GetRepository<Entity.DBContent.VAITRO>().Find(x => x.Id == request.Id);
                if (data == null)
                    throw new Exception("Không tìm thấy thông tin");
                else
                {
                    result = _mapper.Map<MODELVaiTro>(data);
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
        public BaseResponse<PostVaiTroRequest> GetByPost(GetByIdRequest request)
        {
            var response = new BaseResponse<PostVaiTroRequest>();
            try
            {
                var result = new PostVaiTroRequest();
                var data = _unitOfWork.GetRepository<Entity.DBContent.VAITRO>().Find(x => x.Id == request.Id);
                if (data == null)
                {
                    result.Id = Guid.NewGuid();
                    result.IsEdit = false;
                }
                else
                {
                    result = _mapper.Map<PostVaiTroRequest>(data);
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
        public BaseResponse<MODELVaiTro> Insert(PostVaiTroRequest request)
        {
            var response = new BaseResponse<MODELVaiTro>();
            try
            {
                var add = _mapper.Map<Entity.DBContent.VAITRO>(request);
                add.Id = Guid.NewGuid();
                add.NguoiTao = "";
                add.NgayTao = DateTime.Now;
                add.NguoSua = _contextAccessor.HttpContext.User.Identity.Name;
                add.NgaySua = DateTime.Now;

                _unitOfWork.GetRepository<Entity.DBContent.VAITRO>().Add(add);
                _unitOfWork.Commit();

                response.Data = _mapper.Map<MODELVaiTro>(add);
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }

            return response;
        }

        //UPDATE
        public BaseResponse<MODELVaiTro> Update(PostVaiTroRequest request)
        {
            var response = new BaseResponse<MODELVaiTro>();
            try
            {
                var update = _unitOfWork.GetRepository<Entity.DBContent.VAITRO>().Find(x => x.Id == request.Id && !x.IsDeleted);
                if (update != null)
                {
                    _mapper.Map(request, update);
                    update.NguoSua = _contextAccessor.HttpContext.User.Identity.Name;
                    update.NgaySua = DateTime.Now;

                    _unitOfWork.GetRepository<Entity.DBContent.VAITRO>().Update(update);
                    _unitOfWork.Commit();

                    response.Data = _mapper.Map<MODELVaiTro>(update);
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

        //DELETE LIST
        public BaseResponse<string> DeleteList(DeleteListRequest request)
        {
            var response = new BaseResponse<string>();
            try
            {
                foreach (var id in request.Ids)
                {
                    var delete = _unitOfWork.GetRepository<Entity.DBContent.VAITRO>().Find(x => x.Id == id);
                    if (delete != null)
                    {
                        delete.IsDeleted = true;
                        delete.NguoiXoa = _contextAccessor.HttpContext.User.Identity.Name;
                        delete.NgayXoa = DateTime.Now;

                        _unitOfWork.GetRepository<Entity.DBContent.VAITRO>().Update(delete);
                    }
                    else
                    {
                        throw new Exception("Không tìm thấy dữ liệu");
                    }
                }
                _unitOfWork.Commit();
                response.Data = String.Join(",", request);
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
            BaseResponse<List<MODELCombobox>> response = new BaseResponse<List<MODELCombobox>>();
            var data = _unitOfWork.GetRepository<Entity.DBContent.VAITRO>().GetAll(x => x.IsDeleted == false).ToList();
            response.Data = data.Select(x => new MODELCombobox
            {
                Text = x.TenGoi,
                Value = x.Id.ToString(),
            }).OrderBy(x => x.Text).ToList();

            return response;
        }

        //GET LIST PHÂN QUYỀN VAI TRÒ
        public BaseResponse<List<MODELVaiTro_PhanQuyen>> GetListPhanQuyenVaiTro(GetListPhanQuyenVaiTroRequest request)
        {
            var response = new BaseResponse<List<MODELVaiTro_PhanQuyen>>();
            try
            {
                var parameters = new[]
                {
                     new SqlParameter("@NhomId", request.NhomId),
                     new SqlParameter("@VaiTroId", request.VaiTroIdWithNhomQuyen),
                };

                response.Data = _unitOfWork.GetRepository<MODELVaiTro_PhanQuyen>().ExcuteStoredProcedure("sp_TaiKhoan_LayDanhSachPhanQuyen", parameters).ToList();
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }

            return response;
        }

        //POST PHÂN QUYỀN VAI TRÒ
        public BaseResponse<MODELVaiTro_PhanQuyen> PostPhanQuyenVaiTro(PostPhanQuyenVaiTroRequest request)
        {
            var response = new BaseResponse<MODELVaiTro_PhanQuyen>();
            try
            {
                var resultUpdate = _unitOfWork.GetRepository<Entity.DBContent.PHANQUYEN>().Find(x => x.Id == request.Id);
                if (resultUpdate == null)
                {
                    request.Id = Guid.NewGuid();
                    var add = _mapper.Map<Entity.DBContent.PHANQUYEN>(request);
                    _unitOfWork.GetRepository<Entity.DBContent.PHANQUYEN>().Add(add);
                    _unitOfWork.Commit();
                    response.Data = _mapper.Map<MODELVaiTro_PhanQuyen>(add);
                }
                else
                {
                    resultUpdate.IsCapNhat = request.IsCapNhat;
                    resultUpdate.IsDuyet = request.IsDuyet;
                    resultUpdate.IsThem = request.IsThem;
                    resultUpdate.IsThongKe = request.IsThongKe;
                    resultUpdate.IsXem = request.IsXem;
                    resultUpdate.IsXoa = request.IsXoa;
                    _unitOfWork.GetRepository<Entity.DBContent.PHANQUYEN>().Update(resultUpdate);
                    _unitOfWork.Commit();
                    response.Data = _mapper.Map<MODELVaiTro_PhanQuyen>(resultUpdate);
                }
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }

            return response;
        }

  

    }
}
