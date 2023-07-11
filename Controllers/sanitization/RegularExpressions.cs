namespace WebApi.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;
using WebApi.Models.Users;
using System.Text.RegularExpressions;

[ApiController]
[Route("sanitize")]
public class SanitizeREUserController : ControllerBase
{

    private IUserService _userService;
    private IMapper _mapper;

    public SanitizeREUserController(IUserService userService,IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpPost("unsecure")]
    public IActionResult CreateUnsecureRE(CreateRequest model)
    {
        _userService.Create(model);
        return Ok(new { message = model });
    }

    [HttpPost("secure")]
    public IActionResult CreateSecureRE(CreateRequest model)
    {
        string patternName = @"^[a-zA-Z]+$";
        Regex rgxName = new Regex(patternName);
        if (!rgxName.IsMatch(model.FirstName) || !rgxName.IsMatch(model.LastName))
        {
            return BadRequest(new { message = "Invalid input" });
        }
        string patternTitle = @"^(Ms|Mr)$";
        Regex rgxTitle = new Regex(patternTitle);
        if (!rgxTitle.IsMatch(model.Title))
        {
            return BadRequest(new { message = "Invalid input" });
        }
        string patternPwd = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$";
        Regex rgxPwd = new Regex(patternPwd);   
        if (!rgxPwd.IsMatch(model.Password))
        {
            return BadRequest(new { message = "Invalid input" });
        }
        return Ok(new { message = model });
    }
    
    [HttpGet("headers/{id}")]
    public IActionResult ExampleHeaders(int id)
    {
        //HttpContext.Response.Headers.Add("X-XSS-Protection", "1");
        //HttpContext.Response.Headers.Add("Content-Security-Policy", "default-src 'self'");
        
        var user = _userService.GetById(id);
        string html = "<div>Bienvenido "+user.FirstName+"</div>";
        return base.Content(html, "text/html");
    }

    [HttpGet("headers/cors")]
    public IActionResult ExampleHeadersCors()
    {
        var html = System.IO.File.ReadAllText(@"./Files/CORS.html");
        return base.Content(html, "text/html");
    }

    [HttpGet("headers/cors/data")]
    public IActionResult ExampleHeadersCorsData()
    {        
        return Ok("Hola a todos!!");
    }
}
