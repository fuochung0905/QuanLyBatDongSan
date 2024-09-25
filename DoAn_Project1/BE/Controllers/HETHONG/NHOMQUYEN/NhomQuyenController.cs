using BE.Helper;
using Microsoft.AspNetCore.Mvc;
using Model.BASE;
using Model.HETHONG.NHOMQUYEN.Requests;
using Service.NHOMQUYEN;

namespace BE.Controllers.HETHONG.NHOMQUYEN
{
    [ApiController]
    [Route("api/[controller]")]
    public class NhomQuyenController : Controller
    {
        private INHOMQUYENService _service;

        public NhomQuyenController(INHOMQUYENService service)
        {
            _service = service;
        }
        [HttpPost, Route("get-list-paging")]
        public IActionResult GetListPaging(GetListPagingRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception(Model.COMMON.CommonFunc.GetModelStateAPI(ModelState));
                }

                var result = _service.GetList(request);
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

        [HttpPost, Route("get-by-post")]
        public IActionResult GetByPost(GetByIdRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception(Model.COMMON.CommonFunc.GetModelStateAPI(ModelState));
                }

                var result = _service.GetByPost(request);
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
        [HttpPost, Route("insert")]
        public IActionResult Insert(PostNhomQuyenRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception(Model.COMMON.CommonFunc.GetModelStateAPI(ModelState));
                }
                var result = _service.Insert(request);
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
