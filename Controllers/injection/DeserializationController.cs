namespace WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using WebApi.Services;
using WebApi.Models;

using Newtonsoft.Json;

[ApiController]
[Route("deserialization")]

public class DesController : ControllerBase
{
    [HttpGet]
    public String Get()
    {
        return "Hello";
    }

    [HttpPost]
    public IActionResult JsonStringBody()
    {
        
        // Read the request body into a string with readasync and set AllowSynchronousIO to true 
        
        var body = new System.IO.StreamReader(Request.Body).ReadToEndAsync().Result;
        dynamic obj = JsonConvert.DeserializeObject<dynamic>(body, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto
        });
        return Ok(obj);

    }
   
} 