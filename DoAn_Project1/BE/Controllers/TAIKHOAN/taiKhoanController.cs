using BE.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.HETHONG.TAIKHOAN.Reuquest;
using Service.TAIKHOAN;

namespace BE.Controllers.TAIKHOAN
{
	[ApiController]
	[Route("api/[controller]")]
	public class taiKhoanController : ControllerBase
	{
		private ITAIKHOANService _service;
		private IHttpContextAccessor _contextAccessor;

		public taiKhoanController(ITAIKHOANService service, IHttpContextAccessor contextAccessor)
		{
			_service = service;
			_contextAccessor = contextAccessor;
		}

		[HttpPost, Route("login")]
		[AllowAnonymous]
		public IActionResult Login(LoginRequest request)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return Ok(new ApiResponse(false, (int)Model.COMMON.StatusCode.InternalError,
						Model.COMMON.CommonFunc.GetModelStateAPI(ModelState)));
				}
				var result = _service.Login(request);
				if (result.Error)
				{
					throw new Exception(result.Message);
				}
				else
				{
					HttpContext.Response.Cookies.Append("token", result.Data.TaiKhoan.Token ?? "", new CookieOptions
					{
						HttpOnly = true,
						SameSite = SameSiteMode.Strict,
						Secure = true,
						IsEssential = true
				
					});
					return Ok(new ApiOkResponse(result.Data));
				}
			}
			catch (Exception ex)
			{
				return Ok(new ApiResponse(false, (int)Model.COMMON.StatusCode.NotImplemented, ex.Message));
			}
		}

        [HttpPost]
        [Route("logout")]
        public IActionResult Logout()
        {
			Response.Cookies.Delete("token");
            return Ok(new { success = true, message = "Logout thành công." });
        }
	}
}
