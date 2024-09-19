using System.Net.Http.Headers;
using FE.Helper;
using FE.Models;
using Microsoft.AspNetCore.Mvc;
using Model.BASE;
using Model.HETHONG.TAIKHOAN.Dtos;
using Newtonsoft.Json;

namespace FE.Controllers
{
   
    public class BaseController<T> : Controller
    {
	    private ICachService _cachService;
        public BaseController()
        {
	        _cachService = new InMemoryCache();
        }

        public ResponseData LoginAPI(string action, object model)
        {
	        ResponseData response = new ResponseData();
	        try
	        {
		        using (var client = new HttpClient())
		        {
			        client.BaseAddress = new Uri("https://localhost:7078/api/");
                    client.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Accept.Clear();
                    var responseTask = client.PostAsJsonAsync(action, model);
                    responseTask.Wait();
                    response = ExecuteApiResponse(responseTask);
		        }
	        }
	        catch (Exception e)
	        {
		        response.Status = false;
		        response.Message = Model.COMMON.CustomException.ConvertExceptionToMessage(e, "Lỗi hệ thống");

	        }

	        return response;
        }
        public ResponseData GetAPI(string action)
        {
	        ResponseData response = new ResponseData();
	        try
	        {
		        using (var client = new HttpClient())
		        {
			        client.BaseAddress = new Uri("https://localhost:7078/api/");
			        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetToken());
			        var responseTask = client.GetAsync(action);
			        responseTask.Wait();
			        response = ExecuteApiResponse(responseTask);
		        }
	        }
	        catch (Exception ex)
	        {
		        response.Status = false;
		        response.Message = Model.COMMON.CustomException.ConvertExceptionToMessage(ex, "Lỗi hệ thống");
	        }
            return response;
        }
        public ResponseData PostAPI<T>(string action, T model)
        {
            ResponseData response = new ResponseData();
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromMinutes(5);
                    client.BaseAddress = new Uri("https://localhost:7078/api/");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetToken());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Accept.Clear();
                    var responseTask = client.PostAsJsonAsync(action, model);
                    responseTask.Wait();
                    response = ExecuteApiResponse(responseTask);
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = Model.COMMON.CustomException.ConvertExceptionToMessage(ex, "Lỗi hệ thống");
            }

            return response;
        }

        public ResponseData ExecuteApiResponse(Task<HttpResponseMessage> responseTask)
        {
            ResponseData response = new ResponseData();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readeTask = result.Content.ReadAsStringAsync();
                readeTask.Wait();
                if (readeTask == null)
                {
                    response.Status = false;
                    response.Message = "Lỗi hệ thống";

                }
                else
                {
                    string json = readeTask.Result;
                    var resultData = JsonConvert.DeserializeObject<MODELAPIBASIC>(json);
                    response.Message = resultData.Message;
                    if (!resultData.Success ||
                        resultData.StatusCode != Convert.ToInt32(Model.COMMON.StatusCode.Success))
                    {
                        response.Status = false;
                    }
                    else
                    {
                        response.Data = resultData.Result;
                    }
                }
            }
            else
            {
                response.Status = false;
                response.Message = "Lỗi hệ thống";
            }
            return response;
        }

        public ResponseData PostFormDataAPI(string action, System.Net.Http.MultipartFormDataContent content)
        {
	        ResponseData response = new ResponseData();
	        try
	        {
		        using (var client = new HttpClient())
		        {
			        client.BaseAddress = new Uri("https://localhost:7078/api/");
			        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetToken());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
                    client.DefaultRequestHeaders.Accept.Clear();
                    var responseTask = client.PostAsync(action, content);
                    responseTask.Wait();
                    response = ExecuteApiResponse(responseTask);
		        }
	        }
	        catch (Exception e)
	        {
		        response.Status = false;
                response.Message = Model.COMMON.CustomException.ConvertExceptionToMessage(e, "Lỗi hệ thống");
	        }

	        return response;
        }

        private string GetToken()
        {
	        string cacheInfo = _cachService.Get<string>(GetUserName() + "_info");
	        if (!string.IsNullOrWhiteSpace(cacheInfo))
	        {
		        return JsonConvert.DeserializeObject<MODELTaiKhoan>(cacheInfo).Token;
	        }

	        return "";
        }

        public string GetUserName()
        {
	        try
	        {
		        if (User != null && User.Identity != null && User.Identity.Name != null)
		        {
                    return User.Identity.Name;
		        }
		        else
		        {
			        throw new Exception("Vui lòng đăng nhập");
		        }
	        }
	        catch (Exception e)
	        {
		        throw;
	        }
        }
        private string GetBEUrl()
        {
	        return new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("BEUrl").Value.ToString();
        }
	}
}
