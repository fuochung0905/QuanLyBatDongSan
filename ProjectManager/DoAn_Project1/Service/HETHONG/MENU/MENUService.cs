using AutoDependencyRegistration.Attributes;
using AutoMapper;
using Microsoft.Data.SqlClient;
using Model.BASE;
using Model.HETHONG.MENU.Dtos;
using Model.HETHONG.MENU.Requests;
using Repository;

namespace Service.HETHONG.MENU
{
    [RegisterClassAsTransient]
    public class MENUService : IMENUService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public MENUService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public BaseResponse<List<MODELMenu>> GetAll()
        {
            var response = new BaseResponse<List<MODELMenu>>();
            try
            {
                var data = _unitOfWork.GetRepository<Entity.DBContent.SYS_MENU>().GetAll(x => x.IsActived);
                response.Data = _mapper.Map<List<MODELMenu>>(data);
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }

            return response;
        }

        public BaseResponse<MODELMenu> GetById(GetMenuByIdRequest request)
        {
            var response = new BaseResponse<MODELMenu>();
            try
            {
                var result = new MODELMenu();
                var data = _unitOfWork.GetRepository<Entity.DBContent.SYS_MENU>().Find(x => x.ControllerName == request.ControllerName);
                if (data == null)
                    throw new Exception("Không tìm thấy thông tin");
                else
                {
                    result = _mapper.Map<MODELMenu>(data);
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

        public BaseResponse<PostMenuRequest> GetByPost(GetMenuByIdRequest request)
        {
            var response = new BaseResponse<PostMenuRequest>();
            try
            {
                var result = new PostMenuRequest();
                var data = _unitOfWork.GetRepository<Entity.DBContent.SYS_MENU>().Find(x => x.ControllerName == request.ControllerName);
                if (data == null)
                {
                    result.IsEdit = false;
                }
                else
                {
                    result = _mapper.Map<PostMenuRequest>(data);
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

        public BaseResponse<GetListPagingResponse> GetListPaging(GetListPagingRequest request)
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

                var result = _unitOfWork.GetRepository<MODELMenu>().ExcuteStoredProcedure("sp_SYS_MENU_GetListPaging", parameters).ToList();
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

        public BaseResponse<MODELMenu> Insert(PostMenuRequest request)
        {
            var response = new BaseResponse<MODELMenu>();
            try
            {
                request.ControllerName = request.ControllerName.ToLower().Trim();
                request.Controller = request.Controller.ToLower().Trim();
                var add = _mapper.Map<Entity.DBContent.SYS_MENU>(request);

                _unitOfWork.GetRepository<Entity.DBContent.SYS_MENU>().Add(add);
                _unitOfWork.Commit();

                response.Data = _mapper.Map<MODELMenu>(add);
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }

            return response;
        }

        public BaseResponse<MODELMenu> Update(PostMenuRequest request)
        {
            var response = new BaseResponse<MODELMenu>();
            try
            {
                request.Controller = request.Controller.Trim();
                request.ControllerName = request.ControllerName.Trim();
                var update = _unitOfWork.GetRepository<Entity.DBContent.SYS_MENU>().Find(x => x.ControllerName == request.ControllerName);
                if (update != null)
                {
                    request.ControllerName = request.ControllerName.Trim();
                    _mapper.Map(request, update);
                    _unitOfWork.GetRepository<Entity.DBContent.SYS_MENU>().Update(update);
                    _unitOfWork.Commit();

                    response.Data = _mapper.Map<MODELMenu>(update);
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
    }
}
