using AutoDependencyRegistration.Attributes;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Model.BASE;
using Model.HETHONG.VAITRO.Dtos;
using Model.HETHONG.VAITRO.Requests;
using Repository;

namespace Service.VAITRO
{
    [RegisterClassAsTransient]
    public class VAITROService : IVAITROService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IHttpContextAccessor _contextAccessor;

        public VAITROService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            this._unitOfWork = unitOfWork;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

        public BaseResponse<GetListPagingResponse> GetListPaging(GetListPagingRequest request)
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
                var result = _unitOfWork.GetRepository<MODELVaiTro>()
                    .ExcuteStoredProcedure("sp_HT_VaiTro_GetListPaging", parameters).ToList();
                GetListPagingResponse responseData = new GetListPagingResponse();
                responseData.Data = result;
                responseData.PageIndex = request.PageIndex;
                responseData.TotalRow = Convert.ToInt32(iTotalRow.Value);

                response.Data = responseData;

            }
            catch (Exception ex)
            {

                response.Error = true;
                response.Message = ex.Message;
            }

            return response;
        }

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

        public BaseResponse<MODELVaiTro> Insert(PostVaiTroRequest request)
        {
            var response = new BaseResponse<MODELVaiTro>();
            try
            {
                var check = _unitOfWork.GetRepository<Entity.DBContent.VAITRO>()
                    .GetAll(x => x.Id != request.Id && x.TenGoi == request.TenGoi);
                if (check != null)
                {
                    throw new Exception("Dữ liệu đã tồn tại");
                }
                var data = _mapper.Map<Entity.DBContent.VAITRO>(request);
                data.NgayTao = DateTime.Now;
                data.NguoiTao = _contextAccessor.HttpContext.User.Identity.Name;
                data.NgaySua = DateTime.Now;
                data.NguoSua = _contextAccessor.HttpContext.User.Identity.Name;
                _unitOfWork.GetRepository<Entity.DBContent.VAITRO>().add(data);
                _unitOfWork.Commit();

                response.Data = _mapper.Map<MODELVaiTro>(data);
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }

            return response;
        }

        public BaseResponse<List<MODELCombobox>> GetAllForCombobox()
        {
            //var response = new BaseResponse<List<MODELCombobox>>();
            //try
            //{
            //    var data = _unitOfWork.GetRepository<Entity.DBContent.VAITRO>().GetAll(x => x.IsActived).ToList();
            //    response.Data = data.Select(x => new MODELCombobox()
            //    {
            //        Text = x.TenGoi,
            //        Value = x.Id.ToString()
            //    }).OrderBy(x => x.Text).ToList();
            //}
            //catch (Exception ex)
            //{
            //    response.Error = true;
            //    response.Message = ex.Message;
            //}
            //return response;
            return null;
        }

        public BaseResponse<string> Delete(DeleteRequest request)
        {
            throw new NotImplementedException();
        }

        public BaseResponse<string> DeleteList(DeleteListRequest request)
        {
            throw new NotImplementedException();
        }

        public BaseResponse<MODELVaiTro> Update(PostVaiTroRequest request)
        {
            var response = new BaseResponse<MODELVaiTro>();
            try
            {
                var data = _unitOfWork.GetRepository<Entity.DBContent.VAITRO>().Find(x => x.Id == request.Id);
                if (data != null)
                {
                    throw new Exception("Dữ liệu đã tồn tại");
                }
                var check = _unitOfWork.GetRepository<Entity.DBContent.VAITRO>()
                    .GetAll(x => x.Id != request.Id && x.TenGoi == request.TenGoi);
                if (check != null)
                {
                    throw new Exception("Tên không được trùng");
                }
                _mapper.Map(request, data);
                data.NgaySua = DateTime.Now;
                data.NguoSua = _contextAccessor.HttpContext.User.Identity.Name;
                _unitOfWork.GetRepository<Entity.DBContent.VAITRO>().update(data);
                _unitOfWork.Commit();
                response.Data = _mapper.Map<MODELVaiTro>(data);
              
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
