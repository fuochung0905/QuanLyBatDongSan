using BE.Helper;
using Microsoft.AspNetCore.Mvc;
using Model.BASE;
using Model.DANHMUC.KHOA.Requests;
using Service.DANHMUC.KHOA;

namespace BE.Controllers.DANHMUC.KHOA
{
    [ApiController]
    [Route("api/[controller]")]
    public class KhoaController : ControllerBase
    {
        private IKHOAService _service;
        public KhoaController(IKHOAService service)
        {
            _service = service;
        }
        [HttpPost, Route("get-list")]
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
            catch (Exception e)
            {

                return Ok(new ApiResponse(false, (int)Model.COMMON.StatusCode.InternalError, e.Message));
            }

        }

        [HttpPost, Route("get-by-id")]
        public IActionResult GetById(GetByIdRequest request)
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
                return Ok(new ApiResponse(false, (int)Model.COMMON.StatusCode.InternalError, ex.Message));
            }
        }

        [HttpPost, Route("insert")]
        public IActionResult Insert(PostKhoanRequest request)
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
        public IActionResult Update(PostKhoanRequest request)
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

        [HttpPost, Route("delete")]
        public IActionResult Delete(DeleteRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception(Model.COMMON.CommonFunc.GetModelStateAPI(ModelState));
                }
                var result = _service.Delete(request);
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

        [HttpPost, Route("delete-list")]
        public IActionResult DeleteList(DeleteListRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception(Model.COMMON.CommonFunc.GetModelStateAPI(ModelState));
                }
                var result = _service.DeleteList(request);
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

        [HttpPost, Route("get-all-combobox")]
        public IActionResult GetAllForCombobox()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception(Model.COMMON.CommonFunc.GetModelStateAPI(ModelState));
                }
                var result = _service.GetAllForCombobox();
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
