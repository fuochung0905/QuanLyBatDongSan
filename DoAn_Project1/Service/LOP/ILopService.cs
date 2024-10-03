using Model.BASE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.LOP
{
    public interface ILopService
    {
        BaseResponse<GetListPagingResponse> GetListPaging(GetListPagingRequest request);
    }
}
