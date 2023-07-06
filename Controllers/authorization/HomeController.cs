namespace WebApi.Controllers;

using AutoMapper;
using WebApi.Models.Users;
using WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class HomeController: ControllerBase
{
    private IUserService _userService;
    private ITokenService _tokenService;
    private IMapper _mapper;

    public HomeController(IUserService userService, ITokenService tokenService, IMapper mapper)
    {
        _userService = userService;
        _tokenService = tokenService;
        _mapper = mapper;
    }

    [Route("login/admin")]
    [HttpPost]
    public IActionResult LoginAdmin(LoginRequest model)
    {
        var user = _userService.GetById(7);

        if (user == null)
            return BadRequest(new { message = "Username or password is incorrect" });

        var tokenString = _tokenService.CreateToken(user);
        return Ok(new{Token = tokenString});
    } 

    [Route("login/user")]
    [HttpPost]
    public IActionResult LoginUser(LoginRequest model)
    {
        var user = _userService.GetById(6);

        if (user == null)
            return BadRequest(new { message = "Username or password is incorrect" });

        var tokenString = _tokenService.CreateToken(user);
        return Ok(new{Token = tokenString});
    } 

    [Route("authorized/admin")]
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult GetAdmin()
    {
        return Ok("Authorized");
    }

    [Authorize]
    [Route("authorized")]
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Authorized");
    }

    [Route("authorized/user")]
    [HttpGet]
    [Authorize(Roles = "User")]
    public IActionResult GetUser()
    {
        return Ok("Authorized");
    }
}