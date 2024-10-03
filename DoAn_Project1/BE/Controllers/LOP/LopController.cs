using BE.Helper;
using Microsoft.AspNetCore.Mvc;
using Model.BASE;
using Service.LOP;

namespace BE.Controllers.HETHONG.LOP
{
    [ApiController]
    [Route("api/[controller]")]
    public class LopController : Controller
    {
        private ILopService _service;
        public LopController (ILopService service)
        {
            _service = service;
        }
        [HttpPost, Route("get-list-paging")]
        public IActionResult GetList(GetListPagingRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception(Model.COMMON.CommonFunc.GetModelStateAPI(ModelState));
                }
                var _result = _service.GetListPaging(request);
                if (_result.Error)
                {
                    throw new Exception(_result.Message);
                }
                else {
                    return Ok(new ApiOkResponse(_result.Data));
                }
            } catch (Exception ex)
            {
                return Ok(new ApiResponse(false, (int)Model.COMMON.StatusCode.InternalError));
            }
        }
    }
}
