using AutoDependencyRegistration.Attributes;
using Microsoft.Data.SqlClient;
using Model.BASE;
using Model.HETHONG.VAITRO.Dtos;
using Repository;

namespace Service.VAITRO
{
    [RegisterClassAsTransient]
    public class VAITROService : IVAITROService
    {
        private IUnitOfWork _unitOfWork;

        public VAITROService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
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
    }
}
