namespace WebApi.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;
using WebApi.Models.Users;
using System.Text.RegularExpressions;

[ApiController]
[Route("sanitizere")]

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
        return Ok(new { message = model });
    }

    [HttpPost("secure")]
    public IActionResult CreateSecureRE(CreateRequest model)
    {
        return Ok(new { message = model });
    }

    [HttpPost("pwd")]
    public IActionResult CreatePwdRE(CreateRequest model)
    {
        return Ok(new { message = model });
    }
    
}
