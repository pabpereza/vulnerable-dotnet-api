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
    public void Post(Deserialization body)
    {
        Console.WriteLine(JsonConvert.DeserializeObject<object>(body.Body,
                // Include TypeNameHandling.All to allow for a more smooth casting and check
                new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All })
        );
    }
}