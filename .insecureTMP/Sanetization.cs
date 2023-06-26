namespace WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using WebApi.Services;
using WebApi.Models;

using Newtonsoft.Json;

[ApiController]
[Route("sanetize")]

public class DesController : ControllerBase
{
    [HttpGet]
    public String Get()
    {
        return "Hello";
    }

   
} 