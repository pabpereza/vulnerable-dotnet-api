namespace WebApi.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("clickjaking")]

public class Clickjaking : ControllerBase
{

    [HttpGet("{id}")]
    public IActionResult GetSite(string id)
    {
		// Generate iframe with the site
		string output = "<iframe src=\"https://"+ id + "\" width=\"100%\" height=\"100%\"></iframe>";

		//Return in html format
		return Content(output, "text/html");
	}

}

