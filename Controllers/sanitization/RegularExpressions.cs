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

    public SanitizeREUserController(
        IUserService userService,
        IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpPost("unsecure")]
    public IActionResult CreateUnsecureRE(CreateRequest model)
    {
        //_userService.Create(model);
        return Ok(new { message = model });
    }

    [HttpPost("secure")]
    public IActionResult CreateSecureRE(CreateRequest model)
    {
        return Ok(new { message = model });
    }
    
    [HttpGet("headers/{id}")]
    public IActionResult ExampleHeaders(int id)
    {
        var user = _userService.GetById(id);
        string html = "<div>My Bienvenido "+user.FirstName+"</div>";
        return base.Content(html, "text/html");
    }
}
