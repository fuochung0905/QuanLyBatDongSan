using AutoDependencyRegistration.Attributes;
using Microsoft.Data.SqlClient;
using Model.BASE;
using Model.HETHONG.LOP.Dtos;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.LOP
{
    [RegisterClassAsTransient]
    public class LopService : ILopService
    {
        private IUnitOfWork _unitOfWork;
        public LopService(IUnitOfWork unitOfWork)
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
                var parameters = new []{
                    new SqlParameter("@iTextSearch", request.TextSearch) ,
                    new SqlParameter("@iPageIndex", request.PageIndex),
                    new SqlParameter("@iRowsPerPage", request.RowPerPage) ,
                    iTotalRow
                };
                var results = _unitOfWork.GetRepository<MODELLop>().ExcuteStoredProcedure("sp_LayDanhSachLop", parameters).ToList();
                GetListPagingResponse responeseData = new GetListPagingResponse();
                responeseData.Data = results;
                responeseData.PageIndex = request.PageIndex;
                responeseData.TotalRow = Convert.ToInt32(iTotalRow.Value);

                response.Data = responeseData;  
            }
            catch (Exception ex) { 
                response.Error = true;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
