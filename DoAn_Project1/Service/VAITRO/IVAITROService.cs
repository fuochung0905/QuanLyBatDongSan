using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.BASE;

namespace Service.VAITRO
{
    public interface IVAITROService
    {
        BaseResponse<GetListPagingResponse> GetListPaging(GetListPagingRequest request);
    }
}
