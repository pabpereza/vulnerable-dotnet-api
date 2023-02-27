namespace WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using WebApi.Services;
using WebApi.Models;

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
    public void Post(Deserialization body)
    {
        Console.WriteLine(body.Body);
    }
}