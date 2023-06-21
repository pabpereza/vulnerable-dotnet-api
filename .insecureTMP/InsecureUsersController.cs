namespace WebApi.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Users;
using WebApi.Services;

[ApiController]
[Route("isusers")]
public class InsecureUsersController : ControllerBase
{
    private IUserService _userService;
    private IMapper _mapper;

    public InsecureUsersController(
        IUserService userService,
        IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }



    // SQL INJECTION
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {

        // Evitar concatenar strings para construir consultas SQL
        string strQry = "SELECT * FROM Users WHERE id='" + id + "'";

        // Usar parámetros en lugar de concatenar strings
        string strQrySecure = "SELECT * FROM Users WHERE id=@id";

        context.Database.ExecuteSqlCommand(strQrySecure, 
            new SqlParameter("@id", id));

        return Ok();
    }




    // OS COMMAND INJECTION
    [HttpGet("{id}")]
    public IActionResult GetByIdOS(int id)
    {

        // Ejecución de procesos sin validación de parámetros
        var process = new System.Diagnostics.Process();
        var startInfo = new System.Diagnostics.ProcessStartInfo();
        startInfo.FileName = "validatedCommand";
        startInfo.Arguments = "validatedArg1 validatedArg2 validatedArg3";
        process.StartInfo = startInfo;
        process.Start();


        // EJECUCIÓN SIN VALIDACIÓN MODERNA
        var info = new System.Diagnostics.ProcessStartInfo("cmd.exe");
        info.ArgumentList.Add("/c");
        info.ArgumentList.Add("dir");
        info.ArgumentList.Add(@"C:\Program Files\dotnet"); // there is no need to escape the space, the API takes care of it

        // or if you prefer collection property initializer syntax:

        var info = new System.Diagnostics.ProcessStartInfo("cmd.exe")
        {
            ArgumentList = {
                "/c",
                "dir",
                @"C:\Program Files\dotnet "
            }
        };

        // The corresponding assignment to the Arguments property is:

        var info = new System.Diagnostics.ProcessStartInfo("cmd.exe")
        {
            Arguments = "/c dir \"C:\\Program Files\\dotnet\""
        };


        // EJECUCIÓN VALIDADA
        string ipAddress = "127.0.0.1";

        //check to make sure an ip address was provided
        if (!string.IsNullOrEmpty(ipAddress))
        {
            // Create an instance of IPAddress for the specified address string (in
            // dotted-quad, or colon-hexadecimal notation).
            if (IPAddress.TryParse(ipAddress, out var address))
            {
            // Display the address in standard notation.
            return address.ToString();
            }
        }

        return Ok();
    }
}