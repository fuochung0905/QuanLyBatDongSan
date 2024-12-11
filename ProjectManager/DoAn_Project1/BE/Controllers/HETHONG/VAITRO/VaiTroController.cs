using BE.Helper;
using Microsoft.AspNetCore.Mvc;
using Model.BASE;
using Model.HETHONG.VAITRO.Requests;
using REPONSITORY.HETHONG.VAITRO;

namespace BE.Controllers.HETHONG
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaiTroController : ControllerBase
    {
        IVAITROService _service;

        public VaiTroController(IVAITROService service)
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
                return Ok(new ApiResponse(false, (int)Model.COMMON.StatusCode.InternalError, ex.Message));
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
        public IActionResult Insert(PostVaiTroRequest request)
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
        public IActionResult Update(PostVaiTroRequest request)
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

        [HttpPost, Route("get-list-phan-quyen")]
        public IActionResult GetListPhanQuyenVaiTro(GetListPhanQuyenVaiTroRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception(Model.COMMON.CommonFunc.GetModelStateAPI(ModelState));
                }
                var result = _service.GetListPhanQuyenVaiTro(request);
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

        [HttpPost, Route("post-phan-quyen")]
        public IActionResult PostPhanQuyenVaiTro(PostPhanQuyenVaiTroRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception(Model.COMMON.CommonFunc.GetModelStateAPI(ModelState));
                }
                var result = _service.PostPhanQuyenVaiTro(request);
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
