using BE.Helper;
using Microsoft.AspNetCore.Mvc;

namespace BE.Controllers.BASE
{
	public class UploadFileController : ControllerBase
	{
		private IWebHostEnvironment _webHostEnvironment;

		public UploadFileController(IWebHostEnvironment webHostEnvironment)
		{
			_webHostEnvironment = webHostEnvironment;
		}

		[HttpPost]
		[RequestSizeLimit(100_000_000)]
		public async Task<IActionResult> Post(List<IFormFile> files, [FromForm] string folderName)
		{
			try
			{
				var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "Files/UploadFile/" + folderName);
				if (!Directory.Exists(folderPath))
				{
					Directory.CreateDirectory(folderPath);
				}

				foreach (var file in files)
				{
					if (file.Length > 0)
					{
						using (var stream = new FileStream(folderPath + "/" + file.Name, FileMode.Create))
						{
							await file.CopyToAsync(stream);
						}
					}
				}

				return Ok(new ApiOkResponse(result: null, success: true,
					statusCode: (int)Model.COMMON.StatusCode.Success, message: ""));
			}
			catch (ArgumentException ex)
			{
				return BadRequest(new ApiResponse(false, (int)Model.COMMON.StatusCode.InternalError, ex.Message));
			}
		}
	}
}
