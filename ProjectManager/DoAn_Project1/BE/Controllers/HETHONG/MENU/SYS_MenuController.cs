using BE.Helper;
using Microsoft.AspNetCore.Mvc;
using Model.BASE;
using Model.HETHONG.MENU.Requests;
using Service.HETHONG.MENU;

namespace BE.Controllers.HETHONG.MENU
{
    [ApiController]
    [Route("api/[controller]")]
    public class SYS_MenuController : ControllerBase
    {
        private IMENUService _service;
        public SYS_MenuController(IMENUService service)
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
                return Ok(new ApiResponse(false, (int)Model.COMMON.StatusCode.InternalError, ex.Message));
            }
        }

        [HttpPost, Route("get-all")]
        public IActionResult GetAll()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception(Model.COMMON.CommonFunc.GetModelStateAPI(ModelState));
                }
                var result = _service.GetAll();
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
                return Ok(new ApiResponse(false, (int)Model.COMMON.StatusCode.InternalError, ex.Message));
            }
        }

        [HttpPost, Route("get-by-id")]
        public IActionResult GetById(GetMenuByIdRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception(Model.COMMON.CommonFunc.GetModelStateAPI(ModelState));
                }
                var result = _service.GetById(request);
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

                return Ok(new ApiResponse(false, (int)Model.COMMON.StatusCode.InternalError, ex.Message));
            }
        }

        [HttpPost, Route("get-by-post")]
        public IActionResult GetByPost(GetMenuByIdRequest request)
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
                return Ok(new ApiResponse(false, (int)Model.COMMON.StatusCode.InternalError, ex.Message));
            }
        }

        [HttpPost, Route("insert")]
        public IActionResult Insert(PostMenuRequest request)
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
                return Ok(new ApiResponse(false, (int)Model.COMMON.StatusCode.InternalError, ex.Message));
            }
        }

        [HttpPost, Route("update")]
        public IActionResult Update(PostMenuRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception(Model.COMMON.CommonFunc.GetModelStateAPI(ModelState));
                }
                var result = _service.Update(request);
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
                return Ok(new ApiResponse(false, (int)Model.COMMON.StatusCode.InternalError, ex.Message));
            }
        }
    }
}
