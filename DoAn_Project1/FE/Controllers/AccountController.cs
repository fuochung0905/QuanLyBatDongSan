using System.Runtime.Caching;
using System.Security.Claims;
using FE.Helper;
using FE.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.COMMON;
using Model.HETHONG.TAIKHOAN.Dtos;
using Model.HETHONG.TAIKHOAN.Reuquest;
using Newtonsoft.Json;

namespace FE.Controllers
{
	public class AccountController : BaseController<AccountController>
	{
		ICachService _cachService;

		public AccountController()
		{
			_cachService = new InMemoryCache();
		}
		[AllowAnonymous]
		public async Task<JsonResult> Login(string UserName, string Password)
		{
			try
			{
				var request = new LoginRequest { UserName = UserName, Password = Password };
				if (request != null && ModelState.IsValid)
				{
					ResponseData response = this.LoginAPI("", request);
					if (response.Status)
					{
						var taiKhoan = JsonConvert.DeserializeObject<MODELTaiKhoanPhanQuyen>(response.Data.ToString());
						var userData = taiKhoan.TaiKhoan;
						var claims = new List<Claim>();
						_cachService.Set(userData.UserName + "_menu", JsonConvert.SerializeObject(taiKhoan.Menu), 60);
						_cachService.Set(userData.UserName + "_role", JsonConvert.SerializeObject(taiKhoan.PhanQuyen), 60);
						_cachService.Set(userData.UserName + "_info", JsonConvert.SerializeObject(taiKhoan.TaiKhoan), 60);
						_cachService.Set(userData.UserName + "_grouprole", JsonConvert.SerializeObject(taiKhoan.NhomQuyen), 60);
						claims.Add(new Claim(ClaimTypes.Name, userData.UserName));

						var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
						var claimPrincipal = new ClaimsPrincipal(claimsIdentity);
						await HttpContext.SignInAsync(claimPrincipal);
						return Json(new { IsSuccess = true, Message = "", Data = taiKhoan.TaiKhoan.VaiTro });
					}
					else
					{
						throw new Exception(response.Message);
					}

				}
				else
				{
					throw new Exception(CommonFunc.GetModelStateAPI(this.ModelState));
				}
			}
			catch (Exception ex)
			{

				return Json(new { IsSuccess = false, Message = "", Data = "" });
			}
		}

		[Authorize]
		public async Task<IActionResult> Logout()
		{
			List<string> cacheKey = MemoryCache.Default.Select(kvp => kvp.Key).ToList();
			foreach (var item in cacheKey)
			{
				MemoryCache.Default.Remove(item);
			}

			await HttpContext.SignOutAsync();
			return RedirectToAction("Index", "Account");
		}
	}
}
