using BE.Helper;
using Microsoft.AspNetCore.Mvc;
using Model.BASE;
using Service.VAITRO;

namespace BE.Controllers.HETHONG.VAITRO
{
    [ApiController]
    [Route("api/[controller]")]
    public class VaiTroController : Controller
    {
        private IVAITROService _service;
        public VaiTroController(IVAITROService service)
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

                var result = _service.GetListPaging(request);
                if (result.Error)
                {
                    throw new Exception(result.Message);
                }
                else
                {
                    return Ok(new ApiOkResponse(result.Data));
                }
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse(false, (int)Model.COMMON.StatusCode.InternalError));

            }
     

        }
        
    }

}
