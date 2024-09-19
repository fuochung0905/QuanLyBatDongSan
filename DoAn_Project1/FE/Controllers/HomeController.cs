using FE.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using FE.Helper;

namespace FE.Controllers
{
    public class HomeController : BaseController<HomeController>
    {
	    private ICachService _cachService;

	    public HomeController( )
	    {
		    _cachService = new InMemoryCache();
	    }

	    [HttpPost]
	    public IActionResult UploadFile(IFormCollection data)
	    {
		    try
		    {
			    var multiForm = new System.Net.Http.MultipartFormDataContent();
			    foreach (var file in data.Files)
			    {
				    multiForm.Add(new StreamContent(file.OpenReadStream()), "files", file.FileName);
			    }
			    multiForm.Add(new StringContent(data["FolderName"]), "FolderName");
			    ResponseData response = this.PostFormDataAPI("", multiForm);
			    if (!response.Status)
			    {
				    return Json(new { IsSuccess = false, Message = response.Message, Data = "" });
			    }

			    return Json(new { IsSuccess = true, Message = "", Data = "" });
			}
		    catch (Exception ex)
		    {
			    string message = "Lỗi upload file "+ ex.Message;
			    return Json(new { IsSuccess = false, Message = message, Data = "" });
		    }
	    }

	    public IActionResult Download(string filePath)
	    {
		    try
		    {
			    string beAddress = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("BEFileUrl").Value.ToString();
			    string fullPath = beAddress + filePath;
			    using (var client = new HttpClient())
			    {
				    using (var result = client.GetAsync(fullPath).Result)
				    {
					    var fileName = Path.GetFileName(fullPath);
						var content = result.Content.ReadAsByteArrayAsync().Result;
						var contentType = "application/octet-stream";
						return File(content, contentType, fileName);
				    }
			    }
		    }
		    catch (Exception ex)
		    {
			 
			    throw;
		    }
	    }
    }
}
