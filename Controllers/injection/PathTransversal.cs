namespace WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("file")]

public class DesPath : ControllerBase
{

	private readonly IWebHostEnvironment _hostingEnvironment;

	[HttpPost]
	public IActionResult Download()
	{
		// Get post data
		var body = new System.IO.StreamReader(Request.Body).ReadToEndAsync().Result;		

		byte[] fileBytes = System.IO.File.ReadAllBytes(body);
		
		return File(fileBytes, "application/octet-stream", body);
	}


} 